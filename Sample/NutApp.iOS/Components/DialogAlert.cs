using System;
using NutApp.Core.Components;
using NutApp.iOS.Extensions;
using UIKit;

namespace NutApp.iOS.Components
{
    public class DialogAlert : IDialogAlert
    {
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
            var instance = UIAlertController.Create(title, text, UIAlertControllerStyle.Alert);

            if (!string.IsNullOrEmpty(positiveText))
            {
                instance.AddAction(UIAlertAction.Create(positiveText, UIAlertActionStyle.Default, _ => positiveAction()));
            }

            if (!string.IsNullOrEmpty(negativeText))
            {
                instance.AddAction(UIAlertAction.Create(negativeText, UIAlertActionStyle.Cancel, _ => negativeAction?.Invoke()));
            }

            var viewController = UIApplication.SharedApplication.GetCurrentViewController();
            if (viewController == null)
            {
                return this;
            }

            var alertViewController = viewController.PresentedViewController as UIAlertController;
            if (alertViewController != null)
            {
                alertViewController.DismissViewController(true, () => { });
            }

            viewController.PresentViewController(instance, true, null);

            return this;
        }
    }
}