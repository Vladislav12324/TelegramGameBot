using Microsoft.AspNetCore.Mvc;


namespace BotServerApplication.Controllers
{
    [Route("/.well-known/pki-validation/E9BB6DFC254B9A03B4F6C16C2FB23480.txt")]
    [ApiController]
    public class TestController : ControllerBase
    {

       private readonly IWebHostEnvironment _appEnvironment;
       
       public TestController(IWebHostEnvironment appEnvironment)
       {
           _appEnvironment = appEnvironment;
       }
       
       [HttpGet]
       public PhysicalFileResult Get()
       {
           string file_path = Path.Combine(_appEnvironment.ContentRootPath, "logs/E9BB6DFC254B9A03B4F6C16C2FB23480.txt");
           // Тип файла - content-type
           string file_type = "application/txt";
           // Имя файла - необязательно
           string file_name = "E9BB6DFC254B9A03B4F6C16C2FB23480.txt";
           return PhysicalFile(file_path, file_type, file_name);
        }
    }
}
