using BotServerApplication.Controllers;
using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;
using TelegramBotGame.Bot;

namespace TelegramBotGame.Runpoint
{
    public class GameBotService
    {
        private TelegramGameBot _telegramGameGameBot;
        
        public GameBotService(TelegramGameBot bot)
        {
            _telegramGameGameBot = bot;

            Console.WriteLine("Bot started " + _telegramGameGameBot.BotClient.GetMeAsync().Result.FirstName);
            

        }

        public void Start()
        {
            StartGameBotReceiving(_telegramGameGameBot);

        }

        private void StartGameBotReceiving(TelegramGameBot gameGameBot)
        {
            var cts = new CancellationTokenSource();
            var cancellationToken = cts.Token;
            
            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = { },
            };
        
            gameGameBot.BotClient.StartReceiving
            (
                TelegramGameBot.HandleUpdateAsync,
                TelegramGameBot.HandleErrorAsync,
                receiverOptions,
                cancellationToken
            );
        }
    }
}

