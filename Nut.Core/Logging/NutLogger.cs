using System;
using Nut.Core.Platform;

namespace Nut.Core.Logging
{
    public abstract class NutLogger : NutSingleton<INutLogger>, INutLogger
    {
        private static long startTicks;

        static NutLogger()
        {
            // Reduce DateTime init lag
            var dummyCall = DateTime.Now.Ticks;
        }

        public virtual void Info(string tag, string message)
        {
            Log(tag, message, NutLoggerLevel.Info);
        }

        public virtual void Warn(string tag, string message)
        {
            Log(tag, message, NutLoggerLevel.Warn);
        }

        public virtual void Error(string tag, string message)
        {
            Log(tag, message, NutLoggerLevel.Error);
        }

        public virtual void Error(string tag, string message, Exception exception)
        {
            Error(tag, $"{message} {exception}");
        }

        public virtual void Debug(string tag, string message)
        {
            Log(tag, message, NutLoggerLevel.Debug);
        }

        public virtual IDisposable Trace(string tag, string message, NutLoggerLevel level)
        {
            var traceTicks = DateTime.UtcNow.Ticks;
            return new NutDisposableCallback(() =>
            {
                var timing = (DateTime.UtcNow.Ticks - traceTicks) / TimeSpan.TicksPerMillisecond;
                Log(tag, $"{message} — {timing}ms", level);
            });
        }

        public virtual void Log(string tag, string message, NutLoggerLevel level)
        {
            var nowTicks = DateTime.Now.Ticks;
            if (startTicks == 0)
            {
                startTicks = nowTicks;
            }

            var timing = (nowTicks - startTicks) / TimeSpan.TicksPerMillisecond;
            Write(tag, $"{timing}ms {message}", level);
        }

        protected abstract void Write(string tag, string message, NutLoggerLevel level);
    }
}