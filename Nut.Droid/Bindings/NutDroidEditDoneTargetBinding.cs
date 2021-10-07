using Android.Views.InputMethods;
using Android.Widget;
using Nut.Core.Bindings;
using Nut.Core.Bindings.Commands;

namespace Nut.Droid.Bindings
{
    public class NutDroidEditDoneTargetBinding : NutTargetCommandBinding
    {
        private EditText target;
        private INutCommand currentCommand;

        public NutDroidEditDoneTargetBinding(EditText target)
        {
            this.target = target;
        }

        protected override void SetCommandValue(INutCommand command)
        {
            currentCommand = command;
            target.EditorAction += HandleEditorAction;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (target != null)
                {
                    target.EditorAction -= HandleEditorAction;
                    target = null;
                }

                currentCommand = null;
            }

            base.Dispose(disposing);
        }

        private void HandleEditorAction(object sender, TextView.EditorActionEventArgs e)
        {
            if (e.ActionId == ImeAction.Done)
            {
                currentCommand?.Execute(((EditText)sender).Text);
            }
        }
    }
}