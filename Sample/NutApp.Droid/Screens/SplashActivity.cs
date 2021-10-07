using Android.App;
using Android.Content.PM;
using Nut.Core.Application;
using Nut.Droid.Screens;

namespace NutApp.Droid.Screens
{
    [Activity(Theme = "@style/SplashTheme", ScreenOrientation = ScreenOrientation.Portrait, MainLauncher = true, NoHistory = true)]
    public class SplashActivity : NutSplashActivity
    {
        public override INutApplicationLauncher CreateLauncher()
        {
            return new Launcher(this);
        }
    }
}