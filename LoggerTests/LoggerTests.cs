using System;
using System.IO;
using System.Net;
using Common.Configurations;
using Common.Enums;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace LoggerTests
{
    public class LoggerTests
    {
        private readonly IOptions<LoggingSettings> _loggingOptions;

        public LoggerTests()
        {
            _loggingOptions = Options.Create(new LoggingSettings());
        }

        [Fact]
        public void Logger_AfterFirstLog_CreatesLogFile()
        {
            var fileName = "logTest";
            var fileExtensions = ".txt";
            _loggingOptions.Value.FileName = fileName + fileExtensions;

            var logger = new Logger.Logger(_loggingOptions);
            logger.Log("test message", LogLevel.Fatal);

            var finalName = $"{fileName}{DateTime.Now:yyyyMMdd}{fileExtensions}";
            var exists = File.Exists(finalName);

            Assert.True(exists);
            //should delete
        }
    }
}