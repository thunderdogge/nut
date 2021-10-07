using Android.Views;

namespace Nut.Droid.Extensions
{
    public static class NutDroidViewExtensions
    {
        public static TView FindFirstOrDefault<TView>(this View root) where TView : View
        {
            if (root == null)
            {
                return null;
            }

            var tView = root as TView;
            if (tView != null)
            {
                return tView;
            }

            var groupView = root as ViewGroup;
            if (groupView == null)
            {
                return null;
            }

            for (var i = 0; i < groupView.ChildCount; i++)
            {
                var subView = groupView.GetChildAt(i);
                var targetView = FindFirstOrDefault<TView>(subView);
                if (targetView != null)
                {
                    return targetView;
                }
            }

            return null;
        }
    }
}