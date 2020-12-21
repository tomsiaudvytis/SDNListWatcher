using Common.Enums;

namespace Common.Interfaces
{
    public interface ILogger
    {
        void Log(string message, LogLevel logLevel);
    }
}