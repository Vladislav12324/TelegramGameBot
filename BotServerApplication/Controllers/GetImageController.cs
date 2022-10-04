using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BotServerApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetImageController : ControllerBase
    {
        [HttpGet]
        public async Task<FileStreamResult> Get(string link)
        {
            if(link != null || link != "")
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(link);
                request.Method = "GET";

                WebResponse response = request.GetResponse();
                Stream stream = response.GetResponseStream();

                FileStreamResult fileStreamResult = new FileStreamResult(stream, "application/octet-stream")
                {
                    FileDownloadName = Path.GetFileName(link)
                };
                return fileStreamResult;
            }
            
            return null;
        }
       }

    
}
