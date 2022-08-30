using BotServerApplication.Controllers;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Types;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using ElmahCore;
using ElmahCore.Mvc;
using Microsoft.AspNetCore.Mvc;

namespace TelegramBotGame.Bot
{
    public class TelegramGameBot
    {
        public TelegramBotClient BotClient;
        

        public TelegramGameBot()
        {
            BotClient = new TelegramBotClient(new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("AppSettings")["BotToken"]);
        }
        
        [NonAction]
        public static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(update));

            if (update.Type == Telegram.Bot.Types.Enums.UpdateType.CallbackQuery)
            {
                //var gameUrlWithParams = string.Format(GameURL + "?userId={0}&messageId={1}&chatId={2}", update.CallbackQuery.From.Id, update.CallbackQuery.InlineMessageId, update.Message.Chat.Id);
                var gameUrlWithParams = string.Format(new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("AppSettings")["GameHref"] + "?userId={0}&messageId={1}&chatId={2}",
                    update.CallbackQuery.From.Id, update.CallbackQuery.InlineMessageId, update.CallbackQuery.ChatInstance);
                botClient.AnswerCallbackQueryAsync(update.CallbackQuery.Id, null, null, gameUrlWithParams);

            }
            else if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
            {
                Console.WriteLine("AAA");
            }
        }

        public static async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(exception));
            ElmahExtensions.RaiseError(exception);
        }
    }


}

