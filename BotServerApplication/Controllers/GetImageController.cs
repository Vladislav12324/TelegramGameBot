using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BotServerApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetImageController : ControllerBase
    {
        [HttpGet]
        public Task<FileStreamResult> Get(string link)
        {
            return StreamDownload(link);
        }

        public async Task<FileStreamResult> StreamDownload(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";

            WebResponse response = request.GetResponse();
            Stream stream = response.GetResponseStream();

            

            return new FileStreamResult(stream, "application/octet-stream")
            {
                FileDownloadName = Path.GetFileName(url)
            };
        }
    }

    
}
