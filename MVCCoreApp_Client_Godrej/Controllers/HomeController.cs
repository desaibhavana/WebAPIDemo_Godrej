using Microsoft.AspNetCore.Mvc;
using MVCCoreApp_Client_Godrej.Models;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;

namespace MVCCoreApp_Client_Godrej.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task< IActionResult> Index()
        {
            List<Product> productList = new List<Product>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:12186/api/product"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    productList = JsonConvert.DeserializeObject<List<Product>>(apiResponse)!;
                }
            }
            return View(productList);

        }


        [HttpGet]
        public IActionResult GetProduct()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> GetProduct(Product product)
        {
            Product returnproduct = new Product();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:12186/api/product/" + product.ProductId))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        returnproduct = JsonConvert.DeserializeObject<Product>(apiResponse)!;
                    }
                    else
                    {
                        ViewBag.StatusCode = response.StatusCode;
                    }
                }
            }
            return View(returnproduct);
        }

        [HttpGet]
        public IActionResult AddProduct()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(Product product)
        {
            Product receivedProduct = new Product();
            using (var httpClient = new HttpClient())
            {
                //Convert Product object into JSON string
                StringContent content = new StringContent(JsonConvert.SerializeObject(product), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("http://localhost:12186/api/product", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    receivedProduct = JsonConvert.DeserializeObject<Product>(apiResponse);
                }
            }
            return View(receivedProduct);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateProduct(int id)
        {
            Product product = new Product();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:12186/api/product/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    product = JsonConvert.DeserializeObject<Product>(apiResponse)!;
                }
            }
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProduct(Product product)
        {
            Product receivedProduct = new Product();
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(product), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PutAsync("http://localhost:12186/api/product/" + product.ProductId, content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    ViewBag.Result = "Success";
                    receivedProduct = JsonConvert.DeserializeObject<Product>(apiResponse)!;
                }
            }
            return View(receivedProduct);
        }

        //Only Post method required
        //When user clik on cross icon image, corrospoding id record will be deleted

        //Not require to create view

        [HttpPost]
        public async Task<IActionResult> DeleteProduct(int ProductId)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.DeleteAsync("http://localhost:12186/api/product/" + ProductId))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                }
            }
            return RedirectToAction("Index");
        }


        [HttpGet]
        public IActionResult AddFile()
        {
            return View();
        }

        [HttpPost] //for add button click
        public async Task<IActionResult> AddFile(Microsoft.AspNetCore.Http.IFormFile file)
        {
            string apiResponse = "";
            using (var httpClient = new HttpClient())
            {
                var form = new MultipartFormDataContent();  //container for content encoded using multipart/form-data MIME type
                using (var fileStream = file.OpenReadStream())
                {
                    form.Add(new StreamContent(fileStream), "file", file.FileName); //Add HTTP content to a collection of HttpContent objects that get serialized to multipart/form-data MIME type.
                    using (var response = await httpClient.PostAsync("http://localhost:12186/api/filedata/UploadFile", form))
                    {
                        response.EnsureSuccessStatusCode();
                        apiResponse = await response.Content.ReadAsStringAsync();
                    }
                }
            }
            return View((object)apiResponse);
        }










        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }




    }
}
/*
 * 
 string ---> json  --- serrialization
json ---> string  --- deserialization

Newtonsoft.Json
System.Type.json

*/
