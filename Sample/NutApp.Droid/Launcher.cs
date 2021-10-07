using Android.Content;
using Nut.Core.Application;
using Nut.Droid;
using NutApp.Core;

namespace NutApp.Droid
{
    public class Launcher : NutDroidLauncher
    {
        private readonly Context context;

        public Launcher(Context context)
        {
            this.context = context;
        }

        protected override INutApplication CreateApplication()
        {
            return new App(new Setup(context));
        }
    }
}