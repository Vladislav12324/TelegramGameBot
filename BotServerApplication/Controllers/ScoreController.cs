using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Telegram.Bot;

namespace BotServerApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScoreController : ControllerBase
    {
       
        [HttpPost]
        public void Post([FromBody] RequestUserData value)
        {
            var BotClient = TelegramBotSingleton.TelegramClient;
            BotClient.SetGameScoreAsync(value.UserId, value.Score,
                value.MessageId);
        }
    }

    
}
