using System;

namespace Nut.Core.Messenger.Runners
{
    public interface INutMessengerRunner
    {
        void Run(Action action);
    }
}