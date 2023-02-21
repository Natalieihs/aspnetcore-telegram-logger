using Telegram.Bot;

public class TelegramBotService
{
    private readonly TelegramBotClient _botClient;

    public TelegramBotService(string botToken)
    {
        _botClient = new TelegramBotClient(botToken);
    }

    public async Task SendMessageAsync(string message, long chatId)
    {
        await _botClient.SendTextMessageAsync(chatId, message);
    }
}
