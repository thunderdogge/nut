using Android.Views;
using Nut.Core.Bindings;
using Nut.Core.Extensions;

namespace Nut.Droid.Bindings
{
    public class NutDroidHiddenTargetBinding : NutTargetBinding
    {
        private readonly View target;

        public NutDroidHiddenTargetBinding(View target)
        {
            this.target = target;
        }

        public override void SetValue(object value)
        {
            var isHidden = NutObjectExtensions.IsTrueValue(value);
            target.Visibility = isHidden ? ViewStates.Invisible : ViewStates.Visible;
        }
    }
}