using Microsoft.AspNetCore.StaticFiles;
using Telegram.Bot;
using Telegram.Bot.Examples.WebHook;
using Telegram.Bot.Examples.WebHook.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHostedService<ConfigureWebhook>();



var botConfig = builder.Configuration.GetSection("BotConfiguration").Get<BotConfiguration>();

// There are several strategies for completing asynchronous tasks during startup.
// Some of them could be found in this article https://andrewlock.net/running-async-tasks-on-app-startup-in-asp-net-core-part-1/
// We are going to use IHostedService to add and later remove Webhook

// Register named HttpClient to get benefits of IHttpClientFactory
// and consume it with ITelegramBotClient typed client.
// More read:
//  https://docs.microsoft.com/en-us/aspnet/core/fundamentals/http-requests?view=aspnetcore-5.0#typed-clients
//  https://docs.microsoft.com/en-us/dotnet/architecture/microservices/implement-resilient-applications/use-httpclientfactory-to-implement-resilient-http-requests
builder.Services.AddHttpClient("tgwebhook")
    .AddTypedClient<ITelegramBotClient>(httpClient => new TelegramBotClient(botConfig.BotToken, httpClient));

// Dummy business-logic service
builder.Services.AddScoped<HandleUpdateService>();

builder.Services.AddControllers().AddNewtonsoftJson();

var app = builder.Build();
app.UseDeveloperExceptionPage();
// The Telegram.Bot library heavily depends on Newtonsoft.Json library to deserialize
// incoming webhook updates and send serialized responses back.
// Read more about adding Newtonsoft.Json to ASP.NET Core pipeline:
//   https://docs.microsoft.com/en-us/aspnet/core/web-api/advanced/formatting?view=aspnetcore-6.0#add-newtonsoftjson-based-json-format-support

var provider = new FileExtensionContentTypeProvider();
provider.Mappings[".unityweb"] = "application/octet-stream";
provider.Mappings[".data"] = "application/octet-stream";
provider.Mappings[".wasm"] = "application/wasm";
provider.Mappings[".symbols.json"] = "application/octet-stream";

app.UseStaticFiles(new StaticFileOptions
{
    ContentTypeProvider = provider
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//app.UseAuthorization();
app.MapControllers();

app.UseRouting();
app.UseCors();

app.UseEndpoints(endpoints =>
{
    // Configure custom endpoint per Telegram API recommendations:
    // https://core.telegram.org/bots/api#setwebhook
    // If you'd like to make sure that the Webhook request comes from Telegram, we recommend
    // using a secret path in the URL, e.g. https://www.example.com/<token>.
    // Since nobody else knows your bot's token, you can be pretty sure it's us.
    var token = botConfig.BotToken;
    endpoints.MapControllerRoute(name: "tgwebhook",
                                 pattern: $"bot/{token}",
                                 new { controller = "Webhook", action = "Post" });
    endpoints.MapControllers();
});

app.Run();