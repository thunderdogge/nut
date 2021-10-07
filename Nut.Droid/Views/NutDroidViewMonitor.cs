using Android.App;
using Android.Content;

namespace Nut.Droid.Views
{
    public class NutDroidViewMonitor : INutDroidViewMonitor
    {
        public Context CurrentContext { get; set; }

        public Activity CurrentActivity => CurrentContext as Activity;
    }
}