using Android.Support.Design.Widget;
using Android.Views;
using Java.Lang;
using Nut.Core.Bindings;
using static Nut.Core.Extensions.NutObjectExtensions;

namespace Nut.Droid.Bindings
{
    public class NutDroidVisibilityTargetBinding : NutTargetBinding
    {
        private readonly View target;
        private readonly BottomSheetBehavior behavior;

        public NutDroidVisibilityTargetBinding(View target)
        {
            this.target = target;
            this.behavior = SafeGetBottomSheetBehavior(target);
        }

        public override void SetValue(object value)
        {
            var isVisible = IsTrueValue(value);
            if (behavior == null)
            {
                target.Visibility = isVisible ? ViewStates.Visible : ViewStates.Gone;
                return;
            }

            behavior.State = isVisible ? BottomSheetBehavior.StateExpanded : BottomSheetBehavior.StateHidden;
        }

        private static BottomSheetBehavior SafeGetBottomSheetBehavior(Object view)
        {
            try
            {
                return BottomSheetBehavior.From(view);
            }
            catch
            {
                return null;
            }
        }
    }
}