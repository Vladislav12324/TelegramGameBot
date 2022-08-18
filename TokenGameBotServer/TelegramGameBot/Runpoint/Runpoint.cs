using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Exceptions;
using TelegramBotGame.GameServer;

namespace TelegramBotGame.Runpoint
{
    class Runpoint
    {
        static void Main(string[] args)
        {
            var gameBotService = new GameBotService();
            var scoreServer = new ScoreServerService();

            gameBotService.Start();
            scoreServer.Start();

            Console.ReadLine();
        }
    }
}

