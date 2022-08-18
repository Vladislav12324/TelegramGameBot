using System.Net;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using LunarLabs.WebServer.Core;
using LunarLabs.WebServer.HTTP;
using Telegram.Bot;
using Telegram.Bot.Types;
using TokenGameBotServer.TelegramGameBot;

namespace TelegramBotGame.GameServer
{
    
    class ScoreServerService
    {
        private Socket serverSocket;
        private HttpListener _listener;

        public ScoreServerService() 
        {
            _listener = new HttpListener();
            _listener.Prefixes.Add(ServerConstants.ServerPath);
        }

        public void Start()
        {
            RunServer();
        }
        
        private async void RunServer()
        {
            _listener.Start();
            Console.WriteLine("Ожидание подключений...");

            while(true)
            {
                HttpListenerContext context = await _listener.GetContextAsync();
                HttpListenerRequest request = context.Request;
                HttpListenerResponse response = context.Response;
                
                if (context.Request.Url.PathAndQuery == "/Data")
                {
                    if (request.HttpMethod == "POST")
                    {
                        StreamReader reader = new StreamReader(request.InputStream);
                        var datajson = reader.ReadToEnd();
                        var userResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<RequestUserData>(datajson);
                        var BotClient = new TelegramBotClient(ServerConstants.BotToken);
                        Console.WriteLine(userResponse.Score);
                        BotClient.SetGameScoreAsync(userResponse.UserId, userResponse.Score,
                            userResponse.MessageId);
                    }
                    else if(request.HttpMethod == "GET")
                    {
                        StreamReader reader = new StreamReader(request.InputStream);
                        var datajson = reader.ReadToEnd();
                        var userResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<RequestUserData>(datajson);
    
                        var BotClient = new TelegramBotClient(ServerConstants.BotToken);
    
                        var hightScores = await BotClient.GetGameHighScoresAsync(userResponse.UserId, userResponse.MessageId);
    
                        var userScoreList = new List<RequestUserData>();
                        
                        foreach (var user in hightScores)
                        {
                            var userData = new UserData(BotClient, user.User.Id, userResponse.MessageId);
                            var requestUserData = new RequestUserData();
                            requestUserData.Score = userData.RequestData.Score;
                            requestUserData.UserName = userData.RequestData.UserName;
                            requestUserData.UserPhotoLink = userData.RequestData.UserPhotoLink;
                            userScoreList.Add(requestUserData);
                        }
                        
                        string data = Newtonsoft.Json.JsonConvert.SerializeObject(userScoreList);
                        byte[] buffer = Encoding.UTF8.GetBytes(data);
                        response.ContentLength64 = buffer.Length;
    
                        using Stream ros = response.OutputStream; 
                        ros.Write(buffer, 0, buffer.Length);
                    }
                }
            }
        }
    }
    
    public class UserData
    {
        private ITelegramBotClient _bot;
        public RequestUserData RequestData;
        
        public UserData(ITelegramBotClient bot, long userId, string messageId)
        {
            RequestData = new RequestUserData();
            _bot = bot;
            RequestData.UserId = userId;
            RequestData.MessageId = messageId;
            RequestData.UserPhotoLink = GetUserAvatarLink()??"";
            var scoreData = GetUserScore();
            RequestData.Score = scoreData.Score;
            RequestData.UserName = scoreData.User.FirstName;
        }

        private GameHighScore GetUserScore()
        {
            var hightScores = _bot.GetGameHighScoresAsync(RequestData.UserId, RequestData.MessageId);
            
            foreach (var scoreUser in hightScores.Result)
            {
                if (scoreUser.User.Id == RequestData.UserId)
                {
                    return scoreUser;
                }
            }

            return null;
        }

        private string GetUserAvatarLink()
        {
            var userPhotos = _bot.GetUserProfilePhotosAsync(RequestData.UserId);
            if (userPhotos.Result.TotalCount > 0)
            {
                var photoId = userPhotos.Result.Photos[0][0].FileId;
                var filePath = _bot.GetFileAsync(photoId).Result.FilePath;
                RequestData.UserPhotoLink = string.Format("https://api.telegram.org/file/bot5518492256:AAGvTK6fMsBdT1Wux_GrhaXOVx-8j_Ee_Qg/{0}", filePath);
                return RequestData.UserPhotoLink;
            }
            return null;
        }
    }
    
    public class RequestUserData
    {
        public long UserId;
        public string MessageId;
        public string UserName;
        public int Score;
        public string UserPhotoLink;
        public long ChatId;
    }
}

