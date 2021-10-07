using Nut.Core.Bindings.Extensions;
using Nut.iOS.Controls;
using NutApp.Core.Screens.Models;
using UIKit;

namespace NutApp.iOS.Screens
{
    public partial class DashboardViewController : BaseViewController<DashboardViewModel>
    {
        public override string Title => "Some todo stuff";

        public DashboardViewController() : base(nameof(DashboardViewController))
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var buttonItem = new UIBarButtonItem(UIBarButtonSystemItem.Add);
            NavigationItem.RightBarButtonItem = buttonItem;

            var tableViewSource = new NutTableViewSource(tableView, ReminderItemCell.Key);

            var bindingSet = this.CreateBindingSet<DashboardViewModel>();
            bindingSet.BindTap(buttonItem).To(x => x.ItemAddCommand);
            bindingSet.BindSource(tableViewSource).To(x => x.Items);
            bindingSet.BindSourceSelect(tableViewSource).To(x => x.ItemSelectCommand);
            bindingSet.Apply();
        }
    }
}
