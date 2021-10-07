using Nut.Core.Models;
using Nut.iOS.Screens;

namespace NutApp.iOS.Screens
{
    public abstract class BaseViewController<TViewModel> : NutViewController<TViewModel> where TViewModel : class, INutViewModel
    {
        protected BaseViewController(string nibName) : base(nibName)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            NavigationController.NavigationBar.Translucent = false;
        }
    }
}