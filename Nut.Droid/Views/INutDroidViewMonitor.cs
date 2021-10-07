using Android.App;
using Android.Content;

namespace Nut.Droid.Views
{
    public interface INutDroidViewMonitor
    {
        Context CurrentContext { get; set; }
        Activity CurrentActivity { get; }
    }
}