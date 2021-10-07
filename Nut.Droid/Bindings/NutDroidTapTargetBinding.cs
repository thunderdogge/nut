using System;
using Android.Views;
using Nut.Core.Bindings;
using Nut.Core.Bindings.Commands;

namespace Nut.Droid.Bindings
{
    public class NutDroidTapTargetBinding : NutTargetCommandBinding
    {
        private View target;
        private INutCommand currentCommand;

        public NutDroidTapTargetBinding(View target)
        {
            this.target = target;
        }

        protected override void SetCommandValue(INutCommand command)
        {
            currentCommand = command;
            target.Click += HandleClick;
        }

        private void HandleClick(object sender, EventArgs e)
        {
            currentCommand?.Execute(sender);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (target != null)
                {
                    target.Click -= HandleClick;
                    target = null;
                }

                currentCommand = null;
            }

            base.Dispose(disposing);
        }
    }
}