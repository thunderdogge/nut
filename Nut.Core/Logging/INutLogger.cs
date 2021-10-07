using System;

namespace Nut.Core.Logging
{
    public interface INutLogger
    {
        void Log(string tag, string message, NutLoggerLevel level);
        void Info(string tag, string message);
        void Warn(string tag, string message);
        void Error(string tag, string message);
        void Error(string tag, string message, Exception exception);
        void Debug(string tag, string message);
        IDisposable Trace(string tag, string message, NutLoggerLevel level);
    }
}