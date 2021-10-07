using Nut.Core.Bindings;
using Nut.Core.Platform;
using Nut.iOS.Extensions;
using UIKit;

namespace Nut.iOS.Bindings
{
    public class NutIosTextColorTargetBinding : NutTargetColorBinding
    {
        private readonly UIView target;

        public NutIosTextColorTargetBinding(UIView target)
        {
            this.target = target;
        }

        protected override void SetColorValue(NutColor color)
        {
            var uiLabel = target as UILabel;
            if (uiLabel != null)
            {
                uiLabel.TextColor = color.ToNative();
                return;
            }

            var uiButton = target as UIButton;
            if (uiButton != null)
            {
                uiButton.SetTitleColor(color.ToNative(), UIControlState.Normal);
            }
        }
    }
}