using System;
using Common.Configurations;
using Common.Enums;
using Microsoft.Extensions.Options;
using Serilog;
using ILogger = Common.Interfaces.ILogger;

namespace Logger
{
    public class Logger : ILogger
    {
        private readonly Serilog.ILogger _logger;

        public Logger(IOptions<LoggingSettings> loggingSettings)
        {
            _logger = new LoggerConfiguration()
                      .WriteTo.File(loggingSettings.Value.FileName, rollingInterval: RollingInterval.Day)
                      .CreateLogger();
        }

        public void Log(string message, LogLevel logLevel)
        {
            switch (logLevel)
            {
                case LogLevel.Debug:
                    _logger.Debug(message);
                    break;
                case LogLevel.Error:
                    _logger.Error(message);
                    break;
                case LogLevel.Fatal:
                    _logger.Fatal(message);
                    break;
                case LogLevel.Information:
                    _logger.Information(message);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(logLevel), logLevel, null);
            }
        }
    }
}