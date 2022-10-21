using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Telegram.Bot;
using Telegram.Bot.Examples.WebHook.Services;

namespace BotServerApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScoreController : ControllerBase
    {
       
        [HttpPost]
        public void Post([FromServices] HandleUpdateService handleUpdateService,[FromBody] RequestUserData value)
        {
            var BotClient = handleUpdateService._botClient;
            BotClient.SetGameScoreAsync(value.UserId, value.Score,
                value.MessageId);
        }
    }

    
}
