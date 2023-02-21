var builder = WebApplication.CreateBuilder(args);
 string botToken = "YOUR_TELEGRAM_BOT_TOKEN";
long chatId = 0;
builder.Services.AddSingleton<ILoggerProvider>(new TelegramLoggerProvider(botToken, chatId));
builder.Services.AddLogging();
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
