using System;
using Nut.Core.Logging;

namespace Nut.Droid.Logging
{
    public class NutDroidLogger : NutLogger
    {
        protected override void Write(string tag, string message, NutLoggerLevel level)
        {
            if (level == NutLoggerLevel.Info)
            {
                Android.Util.Log.Info(tag, message);
                return;
            }

            if (level == NutLoggerLevel.Warn)
            {
                Android.Util.Log.Warn(tag, message);
                return;
            }

            if (level == NutLoggerLevel.Error)
            {
                Android.Util.Log.Error(tag, message);
                return;
            }

            if (level == NutLoggerLevel.Debug)
            {
                Android.Util.Log.Debug(tag, message);
                return;
            }

            throw new ArgumentOutOfRangeException(nameof(level));
        }
    }
}