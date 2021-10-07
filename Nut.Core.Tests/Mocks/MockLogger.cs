using System;
using Nut.Core.Logging;

namespace Nut.Core.Tests.Mocks
{
    public class MockLogger : NutLogger
    {
        protected override void Write(string tag, string message, NutLoggerLevel level)
        {
            Console.WriteLine($"({level}) {tag}: {message}");
        }
    }
}