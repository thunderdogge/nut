using UIKit;

namespace Nut.iOS.Extensions
{
    public static class NutIosViewExtensions
    {
        public static TView FindFirstOrDefault<TView>(this UIView root) where TView : UIView
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

            if (root.Subviews == null || root.Subviews.Length == 0)
            {
                return null;
            }

            foreach (var subView in root.Subviews)
            {
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