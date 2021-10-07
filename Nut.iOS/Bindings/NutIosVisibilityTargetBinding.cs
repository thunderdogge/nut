using Nut.Core.Bindings;
using Nut.Core.Extensions;
using UIKit;

namespace Nut.iOS.Bindings
{
    public class NutIosVisibilityTargetBinding : NutTargetBinding
    {
        private readonly UIView target;

        public NutIosVisibilityTargetBinding(UIView target)
        {
            this.target = target;
        }

        public override void SetValue(object value)
        {
            target.Hidden = !NutObjectExtensions.IsTrueValue(value);
        }
    }
}