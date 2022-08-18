using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Telegram.Bot;

namespace BotServerApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScoreController : ControllerBase
    {
        [HttpGet]
        public string Get([FromBody] RequestUserData data)
        {
            
            var BotClient = new TelegramBotClient(ServerConstants.BotToken);
            var hightScores = BotClient.GetGameHighScoresAsync(data.UserId, data.MessageId);
            var userScoreList = new List<RequestUserData>();
                        
            foreach (var user in hightScores.Result)
            {
                var userData = new UserData(BotClient, user.User.Id, data.MessageId);
                var requestUserData = new RequestUserData();
                requestUserData.Score = userData.RequestData.Score;
                requestUserData.UserName = userData.RequestData.UserName;
                requestUserData.UserPhotoLink = userData.RequestData.UserPhotoLink;
                userScoreList.Add(requestUserData);
            }
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(userScoreList);
            return json;
        }
        
        [HttpPost]
        public void Post([FromBody] RequestUserData value)
        {
            var BotClient = new TelegramBotClient(ServerConstants.BotToken);
            BotClient.SetGameScoreAsync(value.UserId, value.Score,
                value.MessageId);
        }
    }
}
