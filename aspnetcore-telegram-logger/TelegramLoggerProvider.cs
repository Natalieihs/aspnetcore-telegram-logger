public class TelegramLoggerProvider : ILoggerProvider
{
    private readonly string _botToken;
    private readonly long _chatId;

    public TelegramLoggerProvider(string botToken, long chatId)
    {
        _botToken = botToken;
        _chatId = chatId;
    }

    public ILogger CreateLogger(string categoryName)
    {
        return new TelegramLogger(_botToken, _chatId);
    }

    public void Dispose()
    {
    }
}
