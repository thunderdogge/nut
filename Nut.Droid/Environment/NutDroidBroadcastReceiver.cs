using System;
using Android.Content;
using Android.Runtime;
using Nut.Core.Application;
using Nut.Core.Extensions;

namespace Nut.Droid.Environment
{
    public abstract class NutDroidBroadcastReceiver : BroadcastReceiver, INutApplicationEntry
    {
        protected NutDroidBroadcastReceiver()
        {
        }

        protected NutDroidBroadcastReceiver(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }

        public override void OnReceive(Context context, Intent intent)
        {
            this.EnsureReady();
        }

        public abstract INutApplicationLauncher CreateLauncher();
    }
}