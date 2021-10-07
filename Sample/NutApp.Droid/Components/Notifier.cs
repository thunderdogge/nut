using Android.Support.Design.Widget;
using Android.Support.V4.Content;
using Nut.Core.Bindings.Commands;
using Nut.Droid.Extensions;
using Nut.Droid.Views;
using NutApp.Core.Components;
using NutApp.Droid.Extensions;

namespace NutApp.Droid.Components
{
    public class Notifier : INotifier
    {
        private readonly INutDroidViewMonitor viewMonitor;

        public Notifier(INutDroidViewMonitor viewMonitor)
        {
            this.viewMonitor = viewMonitor;
        }

        public void NotifyLong(string message, string actionText = null, INutCommand command = null)
        {
            Notify(message, Snackbar.LengthLong, actionText, command);
        }

        public void NotifyShort(string message, string actionText = null, INutCommand command = null)
        {
            Notify(message, Snackbar.LengthShort, actionText, command);
        }

        private void Notify(string message, int length, string actionText, INutCommand command)
        {
            var activity = viewMonitor.CurrentActivity;
            var rootView = activity?.Window?.DecorView?.RootView;
            if (rootView == null)
            {
                return;
            }

            activity.HideKeyboard();

            var targetView = rootView.FindFirstOrDefault<CoordinatorLayout>() ?? rootView;
            var snackbar = Snackbar.Make(targetView, message, length);
            if (actionText != null && command != null)
            {
                var textColor = ContextCompat.GetColor(activity, Resource.Color.green);
                snackbar.SetActionTextColor(textColor);
                snackbar.SetAction(actionText, _ => command.Execute(null));
            }

            activity.RunOnUiThread(() => snackbar.Show());
        }
    }
}