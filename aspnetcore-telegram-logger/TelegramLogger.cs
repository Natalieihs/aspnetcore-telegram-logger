public class TelegramLogger : ILogger
{
    private readonly string _botToken;
    private readonly long _chatId;
    private readonly TelegramBotService _botService;

    public TelegramLogger(string botToken, long chatId)
    {
        _botToken = botToken;
        _chatId = chatId;
        _botService = new TelegramBotService(botToken);
    }

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
    {
        if (logLevel >= LogLevel.Error && exception != null)
        {
            _botService.SendMessageAsync(exception.Message, _chatId).Wait();
        }
    }

    public bool IsEnabled(LogLevel logLevel)
    {
        return true;
    }

    public IDisposable BeginScope<TState>(TState state)
    {
        return null;
    }
}
