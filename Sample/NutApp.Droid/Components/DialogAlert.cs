using System;
using Android.App;
using Nut.Droid.Views;
using NutApp.Core.Components;

namespace NutApp.Droid.Components
{
    public class DialogAlert : IDialogAlert
    {
        private readonly INutDroidViewMonitor viewMonitor;

        public DialogAlert(INutDroidViewMonitor viewMonitor)
        {
            this.viewMonitor = viewMonitor;
        }

        public IDialogAlert Show(string title, string text)
        {
            return Show(title, text, null, null);
        }

        public IDialogAlert Show(string title, string text, string positiveText, Action positiveAction)
        {
            return Show(title, text, positiveText, positiveAction, "Cancel", null);
        }

        public IDialogAlert Show(string title, string text, string positiveText, Action positiveAction, string negativeText, Action negativeAction)
        {
            var instance = new AlertDialog.Builder(viewMonitor.CurrentActivity);
            instance.SetCancelable(true);

            if (!string.IsNullOrEmpty(title))
            {
                instance.SetTitle(title);
            }
            if (!string.IsNullOrEmpty(text))
            {
                instance.SetMessage(text);
            }
            if (!string.IsNullOrEmpty(positiveText))
            {
                instance.SetPositiveButton(positiveText, (s, e) => positiveAction());
            }
            if (!string.IsNullOrEmpty(negativeText))
            {
                instance.SetNegativeButton(negativeText, (s, e) => negativeAction?.Invoke());
            }

            instance.Show();
            return this;
        }
    }
}