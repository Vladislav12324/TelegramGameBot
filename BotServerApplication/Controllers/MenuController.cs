using Microsoft.AspNetCore.Mvc;

namespace BotServerApplication.Controllers
{
    [Route("/")]
    [ApiController]
    public class MenuController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            var html = System.IO.File.ReadAllText("./App_Data/Index.cshtml");
            return base.Content(html, "text/html");
        }
    }
}

