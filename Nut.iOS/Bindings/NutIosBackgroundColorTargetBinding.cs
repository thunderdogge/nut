using Nut.Core.Bindings;
using Nut.Core.Platform;
using Nut.iOS.Extensions;
using UIKit;

namespace Nut.iOS.Bindings
{
    public class NutIosBackgroundColorTargetBinding : NutTargetColorBinding
    {
        private readonly UIView target;

        public NutIosBackgroundColorTargetBinding(UIView target)
        {
            this.target = target;
        }

        protected override void SetColorValue(NutColor color)
        {
            target.BackgroundColor = color.ToNative();
        }
    }
}