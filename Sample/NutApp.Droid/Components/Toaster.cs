using Android.App;
using Android.Widget;
using NutApp.Core.Components;

namespace NutApp.Droid.Components
{
    public class Toaster : IToaster
    {
        public void ShowLong(string message)
        {
            Show(message, ToastLength.Long);
        }

        public void ShowShort(string message)
        {
            Show(message, ToastLength.Short);
        }

        private static void Show(string message, ToastLength length)
        {
            var context = Application.Context;
            if (context == null)
            {
                return;
            }

            Toast.MakeText(context, message, length).Show();
        }
    }
}