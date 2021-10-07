using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Nut.Core.Application;
using Nut.Core.Extensions;

namespace Nut.Droid.Environment
{
    public abstract class NutDroidIntentService : IntentService, INutApplicationEntry
    {
        protected NutDroidIntentService(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }

        protected NutDroidIntentService(string name) : base(name)
        {
        }

        protected override void OnHandleIntent(Intent intent)
        {
            this.EnsureReady();
        }

        public abstract INutApplicationLauncher CreateLauncher();
    }
}