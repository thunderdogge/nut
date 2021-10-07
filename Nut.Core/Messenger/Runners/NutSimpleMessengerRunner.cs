using System;
using Nut.Ioc;

namespace Nut.Core.Messenger.Runners
{
    [NutIocIgnore]
    public class NutSimpleMessengerRunner : INutMessengerRunner
    {
        public void Run(Action action)
        {
            action?.Invoke();
        }
    }
}