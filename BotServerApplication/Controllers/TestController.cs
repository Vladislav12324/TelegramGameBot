using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using System.Diagnostics;
using System.Net.Mime;
using System.Web;
using Telegram.Bot.Types;
using static System.Net.Mime.MediaTypeNames;


namespace BotServerApplication.Controllers
{
    //[Route("/GameAssets")]
    [ApiController]
    public class TestController : ControllerBase
    {

       private readonly IWebHostEnvironment _appEnvironment;
       
       public TestController(IWebHostEnvironment appEnvironment)
       {
           _appEnvironment = appEnvironment;
       }
       
      //[HttpGet]
      //ublic VirtualFileResult Get(string param)
      //
      //    var filepath = Path.Combine("~/", param);
      //    string contentType;
      //    Console.WriteLine(param.Split(".")[1]);
      //    if (param.Split(".")[1]=="wasm")
      //    {
      //        Console.WriteLine("AAA");
      //        contentType = "application/wasm";
      //    }
      //    else
      //    {
      //        contentType = "text/plain";
      //    }
      //    //new FileExtensionContentTypeProvider().TryGetContentType(param, out var contentType);
      //    return File(filepath, contentType, param);
      //}
    }
}
