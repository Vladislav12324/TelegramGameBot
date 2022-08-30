using Microsoft.AspNetCore.Mvc;
using Telegram.Bot;


namespace BotServerApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetScoreController : ControllerBase
    {

        [HttpPost]
        public string Post([FromBody] RequestUserData data)
        {
            var BotClient = new TelegramBotClient(new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("AppSettings")["BotToken"]);
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
    }
}
