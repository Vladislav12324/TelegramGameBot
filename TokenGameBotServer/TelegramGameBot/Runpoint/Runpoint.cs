using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Exceptions;
using TelegramBotGame.Bot;
using TelegramBotGame.GameServer;

namespace TelegramBotGame.Runpoint
{
    class Runpoint
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Bot started " + TelegramBot.bot.GetMeAsync().Result.FirstName);
            
            var cts = new CancellationTokenSource();
            var cancellationToken = cts.Token;
            
            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = { },
            };
            
            TelegramBot.bot.StartReceiving
            (
                TelegramBot.HandleUpdateAsync,
                TelegramBot.HandleErrorAsync,
                receiverOptions,
                cancellationToken
            );
            
            new ScoreServer(80, "127.0.0.1", args);

            Console.ReadLine();
        }
    }
}

