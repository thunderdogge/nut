using System.Linq;
using UIKit;

namespace NutApp.iOS.Extensions
{
    public static class ApplicationExtensions
    {
        public static UIViewController GetCurrentViewController(this UIApplication application)
        {
            var rootViewController = application.KeyWindow.RootViewController;
            if (rootViewController == null)
            {
                return null;
            }

            var navController = rootViewController as UINavigationController;
            if (navController == null)
            {
                return rootViewController;
            }

            var topController = navController.ViewControllers.LastOrDefault();
            if (topController == null || !topController.IsViewLoaded)
            {
                return null;
            }

            return topController;
        }
    }
}