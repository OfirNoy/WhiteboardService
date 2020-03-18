using System;
using Serilog;

namespace WhiteboardService.Logging
{
    public static class Logger
    {
        private static ILogger _log = new LoggerConfiguration()
                                        .ReadFrom.Configuration(WebApp.Configuration.Raw)
                                        .CreateLogger();

        public static void Debug(string message, params string[] p)
        {
            _log.Debug(message, p);
        }
        public static void Debug(string message)
        {
            _log.Debug(message);
        }
        public static void Warn(string message, params string[] p)
        {
            _log.Warning(message, p);
        }
        public static void Warn(string message)
        {
            _log.Warning(message);
        }
        public static void Error(string message, params string[] p)
        {
            _log.Error(message, p);
        }
        public static void Error(string message)
        {
            _log.Error(message);
        }
        public static void Error(Exception ex, string message = "")
        {
            _log.Error(ex, message);
        }
    }
}