using System;
using System.Threading;
using Android.App;
using Nut.Core.Platform;

namespace Nut.Droid.Platform
{
    public class NutDroidMainThreadDispatcher : INutMainThreadDispatcher
    {
        public void RunMainThreadAction(Action action)
        {
            if (Application.SynchronizationContext == SynchronizationContext.Current)
            {
                action();
            }
            else
            {
                Application.SynchronizationContext.Post(x => action(), null);
            }
        }
    }
}