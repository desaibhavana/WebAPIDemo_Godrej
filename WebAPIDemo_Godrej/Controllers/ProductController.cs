using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

using WebAPIDemo_Godrej.Models;

namespace WebAPIDemo_Godrej.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // [Authorize]
   // [Authorize(Roles = "admin")]
    public class ProductController : ControllerBase
    {

        private readonly WebAPIDBGodrejContext _context;

        public ProductController(WebAPIDBGodrejContext context)
        {
            _context = context;

            //if record in Product table exist return else add few records
            if (_context.Products.Any())
            {
                return;// db has been seeded
            }
            else
            {
                Product product1 = new Product() { ProductId = 1, ProductName = "tea", Price = 50, Qty = 1 };

                Product product2 = new Product() { ProductId = 2, ProductName = "juice", Price = 100, Qty = 1 };

                _context.Products.Add(product1);
                _context.Products.Add(product2);

                _context.SaveChanges();
            }
        }

        // GET: api/Product
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return await _context.Products.ToListAsync();
        }

        //write Get method with Id parameter
        //based on Id value return Product record

        //if Id is 0 or negative retun  Bad Request

        //if Product not found return Not Found

        //else return Product 

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProducts(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            //Product product = await (from  p in _context.Products where p.ProductId == id select p).SingleOrDefaultAsync();  

            Product product = await _context.Products.SingleOrDefaultAsync(p => p.ProductId == id);

            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
            
        }

        // POST: api/Product
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
      
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            _context.Products.Add(product);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }

            return CreatedAtAction("GetProducts", new { id = product.ProductId }, product);  //Status Code - 201
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            if (id != product.ProductId)
            {
                return BadRequest("Cannot modify primary key value");
            }

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {               
                    throw;                
            }

            // return NoContent(); //204
            return Ok(product); //200
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            try
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }

            // return NoContent(); //204
            return Ok("Product deleted"); //200
        }
    }
}
