using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace aspnetcore_telegram_logger
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var configuration = builder.Configuration;  
            builder.Services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.AddConfiguration(configuration.GetSection("Logging"));
                loggingBuilder.AddConsole();
                loggingBuilder.AddDebug();
                loggingBuilder.AddProvider(new TelegramLoggerProvider(configuration));
            });
            var app = builder.Build();
            //测试
            app.MapGet("/", (IServiceProvider serviceProvider) =>
            {
                var _logger = serviceProvider.GetService<ILogger<Program>>();
                // 记录一条信息级别的日志消息
                _logger.LogInformation("This is an information message");

                // 记录一条警告级别的日志消息
                _logger.LogWarning("This is a warning message");

                // 记录一条错误级别的日志消息
                _logger.LogError("This is an error message");
            });

            app.Run();
        }
    }
}