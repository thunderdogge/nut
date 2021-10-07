using System;
using Nut.Core.Platform;
using Nut.Ioc;

namespace Nut.Core.Messenger.Runners
{
    [NutIocIgnore]
    public class NutMainThreadMessengerRunner : INutMessengerRunner
    {
        public void Run(Action action)
        {
            var dispatcher = Nuts.Resolve<INutMainThreadDispatcher>();
            if (dispatcher == null)
            {
                Nuts.Warn("Not able to deliver message - no ui thread dispatcher available");
                return;
            }

            dispatcher.RunMainThreadAction(action);
        }
    }
}