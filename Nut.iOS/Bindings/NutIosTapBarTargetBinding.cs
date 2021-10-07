using System;
using Nut.Core.Bindings;
using Nut.Core.Bindings.Commands;
using UIKit;

namespace Nut.iOS.Bindings
{
    public class NutIosTapBarTargetBinding : NutTargetCommandBinding
    {
        private UIBarButtonItem target;
        private INutCommand currentCommand;

        public NutIosTapBarTargetBinding(UIBarButtonItem target)
        {
            this.target = target;
        }

        protected override void SetCommandValue(INutCommand command)
        {
            currentCommand = command;
            target.Clicked += HandleClick;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (target != null)
                {
                    target.Clicked -= HandleClick;
                    target = null;
                    currentCommand = null;
                }
            }

            base.Dispose(disposing);
        }

        private void HandleClick(object sender, EventArgs e)
        {
            currentCommand?.Execute(sender);
        }
    }
}