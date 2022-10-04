using Microsoft.AspNetCore.Mvc;
using System.Threading;
using Telegram.Bot;
using Telegram.Bot.Examples.WebHook.Services;
using Telegram.Bot.Types;
using static System.Net.Mime.MediaTypeNames;

namespace BotServerApplication.Controllers
{
    public class WebhookController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Post([FromServices] HandleUpdateService handleUpdateService,
                                          [FromBody] Update update)
        {
            Console.WriteLine("AAA");
            await handleUpdateService.EchoAsync(update);

            return Ok();
        }
    }
}

