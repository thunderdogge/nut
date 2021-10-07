using Nut.Core.Bindings;
using Nut.Core.Extensions;
using UIKit;

namespace Nut.iOS.Bindings
{
    public class NutIosHiddenTargetBinding : NutTargetBinding
    {
        private readonly UIView target;

        public NutIosHiddenTargetBinding(UIView target)
        {
            this.target = target;
        }

        public override void SetValue(object value)
        {
            target.Hidden = NutObjectExtensions.IsTrueValue(value);
        }
    }
}