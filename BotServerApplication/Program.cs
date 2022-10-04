using TelegramBotGame.Runpoint;
using ElmahCore;
using ElmahCore.Mvc;
using BotServerApplication.Controllers;
using Telegram.Bot;
using TelegramBotGame.Bot;
using Microsoft.AspNetCore.StaticFiles;
using Telegram.Bot.Examples.WebHook;
using Telegram.Bot.Examples.WebHook.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddScoped<HandleUpdateService>();
builder.Services.AddElmah<XmlFileErrorLog>(o => o.LogPath = "~/logs");

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var botConfig = builder.Configuration.GetSection("BotConfiguration").Get<BotConfiguration>();

builder.Services.AddHostedService<ConfigureWebhook>();
builder.Services.AddHttpClient("tgwebhook")
    .AddTypedClient<ITelegramBotClient>(httpClient => new TelegramBotClient(botConfig.BotToken, httpClient));
var app = builder.Build();

app.UseFileServer(enableDirectoryBrowsing: true);

var provider = new FileExtensionContentTypeProvider();
provider.Mappings[".unityweb"] = "application/unityweb";
provider.Mappings[".data"] = "application/data";
provider.Mappings[".wasm"] = "application/wasm";
provider.Mappings[".symbols.json"] = "text/plain";
provider.Mappings[".png"] = "application/png";

app.UseStaticFiles(new StaticFileOptions
{
    ContentTypeProvider = provider
});


app.UseCors(builder =>
{
    builder.AllowAnyOrigin()
               .AllowAnyHeader()
               .AllowAnyMethod();
}
);
app.UseRouting();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseElmah();
app.UseAuthorization();
app.MapControllers();

//___________________________BOT________________________



app.UseEndpoints(endpoints =>
{
    var token = botConfig.BotToken;
    endpoints.MapControllerRoute(name: "tgwebhook",
                                 pattern: $"bot/{token}",
                                 new { controller = "Menu", action = "Post" });
    endpoints.MapControllers();
});

TelegramBotSingleton.TelegramClient = new TelegramBotClient(new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("AppSettings")["BotToken"]);
//var gameBotService = new GameBotService(new TelegramGameBot());
//gameBotService.Start();

app.Run();