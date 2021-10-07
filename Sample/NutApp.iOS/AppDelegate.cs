using Foundation;
using UIKit;

namespace NutApp.iOS
{
    [Register("AppDelegate")]
    public class AppDelegate : UIApplicationDelegate
    {
        public override UIWindow Window { get; set; }

        public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
        {
            SetupWindowAppearance();
            SetupNavigationBarAppearance();

            var launcher = new Launcher(Window);
            launcher.Launch();

            Window.MakeKeyAndVisible();

            return true;
        }

        private void SetupWindowAppearance()
        {
            Window = new UIWindow(UIScreen.MainScreen.Bounds)
            {
                TintColor = UIColor.FromRGB(18, 155, 242),
                BackgroundColor = UIColor.White
            };
        }

        private static void SetupNavigationBarAppearance()
        {
            var textAttributes = new UITextAttributes
            {
                TextColor = UIColor.Black
            };

            UINavigationBar.Appearance.SetTitleTextAttributes(textAttributes);
            UINavigationBar.Appearance.BarTintColor = UIColor.FromRGB(253, 253, 253);
        }
    }
}
