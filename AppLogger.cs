
using Microsoft.Extensions.Logging;

namespace MyIdentityApp.Services
{
    public class AppLogger
    {
        private static ILoggerFactory? _loggerFactory;

        public static void Configure(ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory;
        }

        public static ILogger CreateLogger<T>() => _loggerFactory?.CreateLogger<T>() ?? throw new InvalidOperationException("Logger not configured");

        public static void LogInfo<T>(string message)
        {
            CreateLogger<T>().LogInformation(message);
        }

        public static void LogError<T>(string message, Exception ex)
        {
            CreateLogger<T>().LogError(ex, message);
        }

        public static void LogWarning<T>(string message)
        {
            CreateLogger<T>().LogWarning(message);
        }

        public static void LogDebug<T>(string message)
        {
            CreateLogger<T>().LogDebug(message);
        }
    }
}
