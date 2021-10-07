using System;
using Nut.Core.Logging;

namespace Nut.iOS.Logging
{
    public class NutIosLogger : NutLogger
    {
        protected override void Write(string tag, string message, NutLoggerLevel level)
        {
            Console.WriteLine($"{tag}: [{level.ToString().ToLower()}] {message}");
        }
    }
}