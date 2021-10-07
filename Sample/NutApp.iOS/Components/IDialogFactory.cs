using NutApp.Core.Components;

namespace NutApp.iOS.Components
{
    public class DialogFactory : IDialogFactory
    {
        public IDialogAlert CreateAlert()
        {
            return new DialogAlert();
        }

        public IDialogConfirm CreateConfirm()
        {
            return new DialogConfirm();
        }
    }
}