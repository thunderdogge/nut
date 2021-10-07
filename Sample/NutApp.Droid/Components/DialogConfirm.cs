using System;
using Android.App;
using Nut.Droid.Views;
using NutApp.Core.Components;

namespace NutApp.Droid.Components
{
    public class DialogConfirm : IDialogConfirm
    {
        private readonly INutDroidViewMonitor viewMonitor;
        private AlertDialog.Builder instance;

        public DialogConfirm(INutDroidViewMonitor viewMonitor)
        {
            this.viewMonitor = viewMonitor;
        }

        public void Show(string message, Action positiveAction)
        {
            Show(message, positiveAction, () => instance?.Dispose());
        }

        public void Show(string message, Action positiveAction, Action negativeAction)
        {
            var currentActivity = viewMonitor.CurrentActivity;
            instance = new AlertDialog.Builder(currentActivity);
            instance.SetCancelable(true);
            instance.SetMessage(message);
            instance.SetPositiveButton("Yep", (s, e) => positiveAction.Invoke());
            instance.SetNegativeButton("Cancel", (s, e) => negativeAction.Invoke());
            instance.Show();
        }
    }
}