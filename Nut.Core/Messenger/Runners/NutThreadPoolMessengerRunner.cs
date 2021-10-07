using System;
using System.Threading.Tasks;
using Nut.Ioc;

namespace Nut.Core.Messenger.Runners
{
    [NutIocIgnore]
    public class NutThreadPoolMessengerRunner : INutMessengerRunner
    {
        public void Run(Action action)
        {
            Task.Run(action);
        }
    }
}