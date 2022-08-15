using System.Text;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBotGame.Bot
{
    public class TelegramBot
    {
        private const string GameURL = "https://immortal-games.online/Games/Em/CoinSmash/";
        public static ITelegramBotClient bot = new TelegramBotClient("5518492256:AAGvTK6fMsBdT1Wux_GrhaXOVx-8j_Ee_Qg");
        
        public static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(update));
            
            if(update.Type == Telegram.Bot.Types.Enums.UpdateType.CallbackQuery)
            {
                var gameUrlWithParams = string.Format(GameURL + "?userId={0}&chatId={1}",
                    update.CallbackQuery.From.Id, update.CallbackQuery.InlineMessageId);
                botClient.AnswerCallbackQueryAsync(update.CallbackQuery.Id, null, null,
                    gameUrlWithParams);
            }
        }
        
        public static async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(exception));
        }
    }
}

