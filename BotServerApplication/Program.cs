using TelegramBotGame.Runpoint;
using ElmahCore;
using ElmahCore.Mvc;
using BotServerApplication.Controllers;
using Telegram.Bot;
using TelegramBotGame.Bot;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddElmah<XmlFileErrorLog>(o => o.LogPath = "~/logs");

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseStaticFiles();
app.UseFileServer(enableDirectoryBrowsing: true);

app.UseCors(builder =>
{
    builder.AllowAnyOrigin()
               .AllowAnyHeader()
               .AllowAnyMethod();
}
);


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseElmah();
app.UseAuthorization();
app.MapControllers();
TelegramBotSingleton.TelegramClient = new TelegramBotClient(new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("AppSettings")["BotToken"]);
var gameBotService = new GameBotService(new TelegramGameBot());
gameBotService.Start();
app.Run();