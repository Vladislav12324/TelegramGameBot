using Microsoft.AspNetCore.Mvc;


namespace BotServerApplication.Controllers
{
    [Route("/.well-known/pki-validation/7CF7ADF97560B703A02047F1EBE42951.txt")]
    [ApiController]
    public class TestController : ControllerBase
    {

       private readonly IWebHostEnvironment _appEnvironment;
       
       public TestController(IWebHostEnvironment appEnvironment)
       {
           _appEnvironment = appEnvironment;
       }
       
       [HttpGet]
       public VirtualFileResult Get()
       {
            var filepath = Path.Combine("~/", "7CF7ADF97560B703A02047F1EBE42951.txt");
            return File(filepath, "text/plain", "7CF7ADF97560B703A02047F1EBE42951.txt");
        }
    }
}
