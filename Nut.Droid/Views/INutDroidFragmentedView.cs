using Android.Support.V4.App;

namespace Nut.Droid.Views
{
    public interface INutDroidFragmentedView
    {
        void ShowFragment(Fragment fragment);
        void HideFragment(Fragment fragment);
    }
}