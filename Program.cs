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
            //����
            app.MapGet("/", (IServiceProvider serviceProvider) =>
            {
                var _logger = serviceProvider.GetService<ILogger<Program>>();
                // ��¼һ����Ϣ�������־��Ϣ
                _logger.LogInformation("This is an information message");

                // ��¼һ�����漶�����־��Ϣ
                _logger.LogWarning("This is a warning message");

                // ��¼һ�����󼶱����־��Ϣ
                _logger.LogError("This is an error message");
            });

            app.Run();
        }
    }
}