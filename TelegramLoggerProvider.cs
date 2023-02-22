using Microsoft.Extensions.Logging;

public class TelegramLoggerProvider : ILoggerProvider
{
    private readonly IConfiguration _config;

    public TelegramLoggerProvider(IConfiguration config)
    {
        _config = config;
    }

    public ILogger CreateLogger(string categoryName)
    {
        return new TelegramLogger(categoryName, _config);
    }

    public void Dispose()
    {
    }
}
