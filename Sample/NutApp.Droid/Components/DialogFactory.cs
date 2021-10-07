using Nut.Droid.Views;
using NutApp.Core.Components;

namespace NutApp.Droid.Components
{
    public class DialogFactory : IDialogFactory
    {
        private readonly INutDroidViewMonitor viewMonitor;

        public DialogFactory(INutDroidViewMonitor viewMonitor)
        {
            this.viewMonitor = viewMonitor;
        }

        public IDialogAlert CreateAlert()
        {
            return new DialogAlert(viewMonitor);
        }

        public IDialogConfirm CreateConfirm()
        {
            return new DialogConfirm(viewMonitor);
        }
    }
}