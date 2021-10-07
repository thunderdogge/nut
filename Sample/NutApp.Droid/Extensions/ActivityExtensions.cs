using Android.App;
using Android.Content;
using Android.OS;
using Android.Views.InputMethods;

namespace NutApp.Droid.Extensions
{
    public static class ActivityExtensions
    {
        public static void HideKeyboard(this Activity activity, IBinder binder = null)
        {
            var imm = (InputMethodManager)activity.GetSystemService(Context.InputMethodService);
            if (imm == null || activity.CurrentFocus == null)
            {
                return;
            }

            imm.HideSoftInputFromWindow(binder ?? activity.CurrentFocus.WindowToken, 0);
        }
    }
}