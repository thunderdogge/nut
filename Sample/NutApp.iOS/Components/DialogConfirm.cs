using System;
using NutApp.Core.Components;
using NutApp.iOS.Extensions;
using UIKit;

namespace NutApp.iOS.Components
{
    public class DialogConfirm : IDialogConfirm
    {
        public void Show(string message, Action positiveAction)
        {
            Show(message, positiveAction, null);
        }

        public void Show(string message, Action positiveAction, Action negativeAction)
        {
            var dialog = UIAlertController.Create(null, message, UIAlertControllerStyle.Alert);
            dialog.AddAction(UIAlertAction.Create("Cancel", UIAlertActionStyle.Cancel, _ => negativeAction?.Invoke()));
            dialog.AddAction(UIAlertAction.Create("Yep", UIAlertActionStyle.Default, _ => positiveAction.Invoke()));

            var controller = UIApplication.SharedApplication.GetCurrentViewController();
            controller?.PresentViewController(dialog, true, null);
        }
    }
}