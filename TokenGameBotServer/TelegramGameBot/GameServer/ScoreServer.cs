using System.Net;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Security.Cryptography;
using LunarLabs.WebServer.Core;
using LunarLabs.WebServer.HTTP;
using Telegram.Bot;

namespace TelegramBotGame.GameServer
{
    
    class ScoreServer
    {
        private Socket serverSocket;

        public ScoreServer(int port, string ip, string[] args) 
        {

            HttpListener listener = new HttpListener();
            listener.Prefixes.Add("http://localhost:8888/");
            listener.Start();
            Console.WriteLine("Ожидание подключений...");
            RunServer(listener);
        }

        private async void RunServer(HttpListener listener)
        {
            while(true)
            {
                HttpListenerContext context = await listener.GetContextAsync();
                HttpListenerRequest request = context.Request;
                HttpListenerResponse response = context.Response;
                using (System.IO.Stream body = request.InputStream) // here we have data
                {
                    using (var reader = new System.IO.StreamReader(body, request.ContentEncoding))
                    {
                        var text = reader.ReadLine();
                        Console.WriteLine(text);

                        var arr = text?.Split('&');
                        if (text != null)
                        {
                            var user = arr?[0].Split("=")[1];
                            var message = arr?[1].Split("=")[1];
                            var score = arr?[2].Split("=")[1];
                            Console.WriteLine(user);
                            Console.WriteLine(message);
                            Console.WriteLine(score);
                            ITelegramBotClient bot = new TelegramBotClient("5518492256:AAGvTK6fMsBdT1Wux_GrhaXOVx-8j_Ee_Qg");
                            bot.SetGameScoreAsync(long.Parse(user), int.Parse(score), message);
                        }
                    }
                }
            }
        }
    }
}

