using System;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

public class TelegramLogger : ILogger
{
    private readonly string _categoryName;
    private readonly IConfiguration _config;

    public TelegramLogger(string categoryName, IConfiguration config)
    {
        _categoryName = categoryName;
        _config = config;
    }

    public IDisposable BeginScope<TState>(TState state)
    {
        return null;
    }

    public bool IsEnabled(LogLevel logLevel)
    {
        return logLevel == LogLevel.Error;
    }

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
    {
        if (IsEnabled(logLevel) && exception != null)
        {
            try
            {
                // 获取Bot Token和聊天ID配置
                string botToken = _config["TelegramBotToken"];
                string chatId = _config["TelegramChatId"];

                // 将异常的消息和堆栈跟踪转换为Telegram格式的消息
                string message = "An exception occurred in category '" + _categoryName + "':\n\n" +
                    "Message: " + exception.Message + "\n" +
                    "Stack trace: " + exception.StackTrace;

                // 创建Telegram Bot API的请求URL
                string requestUrl = $"https://api.telegram.org/bot{botToken}/sendMessage?chat_id={chatId}&text={WebUtility.UrlEncode(message)}";

                // 创建Web请求
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(requestUrl);
                request.Method = "GET";

                // 发送Web请求并获取响应
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                // 将响应流转换为字符串
                Stream stream = response.GetResponseStream();
                StreamReader reader = new StreamReader(stream);
                string responseText = reader.ReadToEnd();

                // 解析响应JSON以检查是否发送成功
                dynamic responseObject = JsonConvert.DeserializeObject(responseText);
                bool success = responseObject.ok;

                if (!success)
                {
                    // 发送失败
                    throw new Exception("Failed to send log message to Telegram: " + responseText);
                }
            }
            catch (Exception ex)
            {
                // 发送异常
                Console.WriteLine("Failed to send exception message to Telegram: " + ex.Message);
            }
        }
    }
}
