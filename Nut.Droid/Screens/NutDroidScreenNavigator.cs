using Android.Content;
using Android.Net;
using Nut.Core.Screens;
using Nut.Droid.Views;

namespace Nut.Droid.Screens
{
    public class NutDroidScreenNavigator : NutScreenNavigator
    {
        private readonly INutDroidViewMonitor viewMonitor;

        public NutDroidScreenNavigator(INutDroidViewMonitor viewMonitor)
        {
            this.viewMonitor = viewMonitor;
        }

        protected override bool NavigateUrlWithScheme(string url)
        {
            return NavigateIntent(url, Intent.ActionView);
        }

        public override bool NavigateEmail(string email, string subject, string signature)
        {
            var intent = CreateNavigationIntent($"mailto:{email}", Intent.ActionSendto);

            if (!string.IsNullOrEmpty(signature))
            {
                intent.PutExtra(Intent.ExtraText, signature);
            }

            if (!string.IsNullOrEmpty(subject))
            {
                intent.PutExtra(Intent.ExtraSubject, subject);
            }

            return NavigateIntent(intent);
        }

        protected override bool NavigatePhoneNumber(string phone)
        {
            return NavigateIntent($"tel:{phone}", Intent.ActionDial);
        }

        public override bool CanNavigateUrl(string url)
        {
            var intent = CreateNavigationIntent(url, Intent.ActionView);
            return CanNavigateIntent(intent);
        }

        private bool CanNavigateIntent(Intent intent)
        {
            var context = viewMonitor.CurrentContext;
            return intent.ResolveActivity(context.PackageManager) != null;
        }

        private bool NavigateIntent(string url, string action)
        {
            var intent = CreateNavigationIntent(url, action);
            return NavigateIntent(intent);
        }

        private bool NavigateIntent(Intent intent)
        {
            if (CanNavigateIntent(intent))
            {
                viewMonitor.CurrentContext.StartActivity(intent);
                return true;
            }

            OnNavigationFailed();
            return false;
        }

        private static Intent CreateNavigationIntent(string url, string action)
        {
            return new Intent(action, Uri.Parse(url));
        }
    }
}