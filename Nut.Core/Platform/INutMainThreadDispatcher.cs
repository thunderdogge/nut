using System;

namespace Nut.Core.Platform
{
    public interface INutMainThreadDispatcher
    {
        void RunMainThreadAction(Action action);
    }
}