using System.Text;
using Foundation;
using Nut.Core.Screens;
using UIKit;

namespace Nut.iOS.Screens
{
    public class NutIosScreenNavigator : NutScreenNavigator
    {
        private volatile bool navigationRequested;

        public override bool NavigateEmail(string email, string subject, string signature)
        {
            var sb = new StringBuilder($"mailto:{email}");
            sb.AppendFormat("?subject={0}", CreateUrlEncodedString(subject));
            sb.AppendFormat("&body={0}", CreateUrlEncodedString(signature));

            return NavigateUrlWithScheme(sb.ToString());
        }

        protected override bool NavigatePhoneNumber(string phone)
        {
            return NavigateUrlWithScheme($"tel://{phone}");
        }

        protected override bool NavigateUrlWithScheme(string url)
        {
            if (navigationRequested)
            {
                return false;
            }

            if (CanNavigateUrl(url))
            {
                navigationRequested = true;

                var nsUrl = NSUrl.FromString(url);

                if (UIDevice.CurrentDevice.CheckSystemVersion(10, 0))
                {
                    var urlOptions = CreateOpenUrlOptions(nsUrl);
                    UIApplication.SharedApplication.OpenUrl(nsUrl, urlOptions, s => navigationRequested = false);
                }
                else
                {
                    UIApplication.SharedApplication.OpenUrl(nsUrl);
                    navigationRequested = false;
                }

                return true;
            }

            OnNavigationFailed();
            return false;
        }

        public override bool CanNavigateUrl(string url)
        {
            var nsUrl = NSUrl.FromString(url);
            if (nsUrl == null)
            {
                return false;
            }

            return UIApplication.SharedApplication.CanOpenUrl(nsUrl);
        }

        protected virtual UIApplicationOpenUrlOptions CreateOpenUrlOptions(NSUrl nsUrl)
        {
            return new UIApplicationOpenUrlOptions();
        }

        private static NSString CreateUrlEncodedString(string subject)
        {
            if (string.IsNullOrEmpty(subject))
            {
                return NSString.Empty;
            }

            var allowedCharacters = NSUrlUtilities_NSCharacterSet.UrlHostAllowedCharacterSet;
            return new NSString(subject).CreateStringByAddingPercentEncoding(allowedCharacters);
        }
    }
}