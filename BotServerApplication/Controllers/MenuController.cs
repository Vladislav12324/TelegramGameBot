using Microsoft.AspNetCore.Mvc;
using System.Threading;
using Telegram.Bot;
using Telegram.Bot.Examples.WebHook.Services;
using Telegram.Bot.Types;
using static System.Net.Mime.MediaTypeNames;

namespace BotServerApplication.Controllers
{
    [ApiController]
    public class MenuController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Post([FromServices] HandleUpdateService handleUpdateService,
                                          [FromBody] Update update)
        {

            await handleUpdateService.EchoAsync(update);

            return Ok();
        }
    }
}

