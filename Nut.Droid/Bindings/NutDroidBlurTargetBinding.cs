using Android.Views;
using Android.Widget;
using Nut.Core.Bindings;
using Nut.Core.Bindings.Commands;

namespace Nut.Droid.Bindings
{
    public class NutDroidBlurTargetBinding : NutTargetCommandBinding
    {
        private EditText target;
        private INutCommand currentCommand;

        public NutDroidBlurTargetBinding(EditText target)
        {
            this.target = target;
        }

        protected override void SetCommandValue(INutCommand command)
        {
            currentCommand = command;
            target.FocusChange += HandleFocusChange;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (target != null)
                {
                    target.FocusChange -= HandleFocusChange;
                    target = null;
                }

                currentCommand = null;
            }

            base.Dispose(disposing);
        }

        private void HandleFocusChange(object s, View.FocusChangeEventArgs args)
        {
            if (!args.HasFocus)
            {
                currentCommand.Execute(((EditText)s).Text);
            }
        }
    }
}