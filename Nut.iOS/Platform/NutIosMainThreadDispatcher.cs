using System;
using System.Threading;
using Nut.Core.Platform;
using UIKit;

namespace Nut.iOS.Platform
{
    public class NutIosMainThreadDispatcher : INutMainThreadDispatcher
    {
        private readonly SynchronizationContext synchronizationContext;

        public NutIosMainThreadDispatcher()
        {
            synchronizationContext = SynchronizationContext.Current;
        }

        public void RunMainThreadAction(Action action)
        {
            if (synchronizationContext == SynchronizationContext.Current)
            {
                action();
            }
            else
            {
                UIApplication.SharedApplication.BeginInvokeOnMainThread(action);
            }
        }
    }
}