using Android.OS;
using Android.Support.V7.App;
using Nut.Core.Application;
using Nut.Core.Extensions;

namespace Nut.Droid.Screens
{
    public abstract class NutSplashActivity : AppCompatActivity, INutApplicationEntry
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            this.Launch();
        }

        public abstract INutApplicationLauncher CreateLauncher();
    }
}