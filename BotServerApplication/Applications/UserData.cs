using Telegram.Bot;
using Telegram.Bot.Types;

namespace BotServerApplication.Controllers;

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
