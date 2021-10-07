using Nut.Core.Bindings;
using Nut.Core.Bindings.Commands;
using UIKit;

namespace Nut.iOS.Bindings
{
    public class NutIosEditDoneTargetBinding : NutTargetCommandBinding
    {
        private UITextField target;

        public NutIosEditDoneTargetBinding(UITextField target)
        {
            this.target = target;
        }

        protected override void SetCommandValue(INutCommand command)
        {
            target.ShouldReturn += x =>
            {
                command?.Execute(x.Text);
                return true;
            };
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (target != null)
                {
                    target.ShouldReturn = null;
                    target = null;
                }
            }

            base.Dispose(disposing);
        }
    }
}