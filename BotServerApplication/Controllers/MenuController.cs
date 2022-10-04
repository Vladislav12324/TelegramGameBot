using Microsoft.AspNetCore.Mvc;
using System.Threading;
using Telegram.Bot;
using Telegram.Bot.Examples.WebHook.Services;
using Telegram.Bot.Types;
using static System.Net.Mime.MediaTypeNames;

namespace BotServerApplication.Controllers
{
    [Route("/")]
    [ApiController]
    public class MenuController : Controller
    {
        [HttpPost]
        public async Task<IActionResult> Post([FromServices] HandleUpdateService handleUpdateService,
                                          [FromBody] Update update)
        {

            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(update));
            
            if (update.Type == Telegram.Bot.Types.Enums.UpdateType.CallbackQuery)
            {
                //var gameUrlWithParams = string.Format(GameURL + "?userId={0}&messageId={1}&chatId={2}", update.CallbackQuery.From.Id, update.CallbackQuery.InlineMessageId, update.Message.Chat.Id);
                var gameUrlWithParams = string.Format(new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("AppSettings")["GameHref"] + "?userId={0}&messageId={1}&chatId={2}",
                    update.CallbackQuery.From.Id, update.CallbackQuery.InlineMessageId, update.CallbackQuery.ChatInstance);
                TelegramBotSingleton.TelegramClient.AnswerCallbackQueryAsync(update.CallbackQuery.Id, null, null, gameUrlWithParams);

            }
            else
            {
                Console.WriteLine("AAA");
            }

            return Ok();
        }
    }
}

