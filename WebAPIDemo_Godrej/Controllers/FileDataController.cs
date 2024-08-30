using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPIDemo_Godrej.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileDataController : ControllerBase
    {
        private IWebHostEnvironment webHostEnvironment;

        public FileDataController(IWebHostEnvironment _webHostEnvironment)
        {
            webHostEnvironment = _webHostEnvironment;
        }

        [HttpPost("UploadFile")]
        public async Task<string> UploadFile([FromForm] IFormFile file)
        {           
            string path = Path.Combine(webHostEnvironment.WebRootPath, "ClientImages/" + file.FileName);
            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            return "http://localhost:12186/ClientImages/" + file.FileName;
        }
    }
}

//The FromForm attribute is for incoming data from a submitted form sent by the content type application/x-www-url-formencoded while the FromBody will parse the model the default way, which in most cases are sent by the content type application/json, from the request body.

//The WebRootPath() method of the IWebHostEnvironment class gives the absolute path of the application’s wwwroot folder

//use the Path.Combine() method of the System.IO namespace to create an absolute path where the file will be saved
//~/wwwroot/clientimages/abc.txt

