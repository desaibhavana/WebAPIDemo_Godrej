using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace WebAPIDemo_Godrej.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class DemoController : ControllerBase
    {
        //[HttpGet]
        //public IActionResult Get()
        //{
        //    return Ok("first api response");
        //}

        static List<string> Countries = new List<string> { "India", "USA", "UK", "Japan", "France" };

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(Countries);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get(int id)
        {
            if (id >= Countries.Count)
            {
                return NotFound(); //404
            }
            string country = Countries[id];
            return Ok(country);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Post([FromBody] string value = "")
        {
            if (value != String.Empty)
            {
                Countries.Add(value);
                return Ok("Country inserted");
            }
            else { return BadRequest(); }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Put(  int id, [FromBody] string value)
        {
            if (id < Countries.Count)
            {
                Countries[id] = value;
                return Ok("country updated");
            }
            else { return NotFound(); }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(int id)
        {
            if (id < Countries.Count)
            {
                Countries.RemoveAt(id);
                return Ok("country deleted");
            }
            else { return NotFound(); }
        }
    }
}
