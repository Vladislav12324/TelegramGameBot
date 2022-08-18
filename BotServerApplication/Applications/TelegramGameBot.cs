using System.Text;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBotGame.Bot
{
    public class TelegramGameBot
    {
        private const string GameURL = "https://compassionate-mclean.31-31-196-202.plesk.page:8172/msdeploy.axd?site=compassionate-mclean.31-31-196-202.plesk.page";
        public TelegramBotClient BotClient;

        public TelegramGameBot()
        {
            BotClient = new TelegramBotClient("5518492256:AAGvTK6fMsBdT1Wux_GrhaXOVx-8j_Ee_Qg");
        }
        
        public static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(update));
            
            if(update.Type == Telegram.Bot.Types.Enums.UpdateType.CallbackQuery)
            {
                //var gameUrlWithParams = string.Format(GameURL + "?userId={0}&messageId={1}&chatId={2}", update.CallbackQuery.From.Id, update.CallbackQuery.InlineMessageId, update.Message.Chat.Id);
                var gameUrlWithParams = string.Format(GameURL + "?userId={0}&messageId={1}&chatId={2}",
                    update.CallbackQuery.From.Id, update.CallbackQuery.InlineMessageId, update.CallbackQuery.ChatInstance);
                botClient.AnswerCallbackQueryAsync(update.CallbackQuery.Id, null, null, gameUrlWithParams);

            }
        }

        public static async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(exception));
        }
    }

    
}

