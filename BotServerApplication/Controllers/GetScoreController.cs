using BotServerApplication.Controllers;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot;
using Telegram.Bot.Examples.WebHook.Services;

namespace BotServerApplication.Controllers
{
    [Route("api/[controller]")]
    public class GetScoreController: ControllerBase
    {
        [HttpPost]
        public string Post([FromServices] HandleUpdateService handleUpdateService,[FromBody] RequestUserData data)
        {
            var BotClient = handleUpdateService._botClient;

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
                Console.WriteLine(userData.RequestData.UserName);
            }
            Console.WriteLine("AAA2");

            string json = Newtonsoft.Json.JsonConvert.SerializeObject(userScoreList);
            Console.WriteLine(json);

            return json;
        }
    }
}
