using Microsoft.AspNetCore.Mvc;
using Telegram.Bot.Examples.WebHook.Services;
using Telegram.Bot.Types;

namespace BotServerApplication.Controllers;

public class WebhookController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Post([FromServices] HandleUpdateService handleUpdateService,
                                          [FromBody] Update update)
<<<<<<< HEAD
        {
            if(update.CallbackQuery != null)
            {

                await handleUpdateService.EchoAsync(update);
            }
            

            return Ok();
        }
=======
    {
        await handleUpdateService.EchoAsync(update);
        return Ok();
    }
    public IActionResult Get()
    {
        return Ok();
>>>>>>> 2fe8eacb1b50ae084a3207bc2e5f8f8af42e1566
    }

}
