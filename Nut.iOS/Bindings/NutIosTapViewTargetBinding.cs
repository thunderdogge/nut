using Nut.Core.Bindings;
using Nut.Core.Bindings.Commands;
using UIKit;

namespace Nut.iOS.Bindings
{
    public class NutIosTapViewTargetBinding : NutTargetCommandBinding
    {
        private UIView target;
        private UITapGestureRecognizer tapRecognizer;

        public NutIosTapViewTargetBinding(UIView target)
        {
            this.target = target;
            this.target.UserInteractionEnabled = true;
        }

        protected override void SetCommandValue(INutCommand command)
        {
            tapRecognizer = new UITapGestureRecognizer(() => command.Execute(null))
            {
                NumberOfTapsRequired = 1,
                NumberOfTouchesRequired = 1,
                CancelsTouchesInView = true
            };

            target.AddGestureRecognizer(tapRecognizer);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (target != null)
                {
                    if (tapRecognizer != null)
                    {
                        target.RemoveGestureRecognizer(tapRecognizer);
                        tapRecognizer = null;
                    }

                    target = null;
                }
            }

            base.Dispose(disposing);
        }
    }
}