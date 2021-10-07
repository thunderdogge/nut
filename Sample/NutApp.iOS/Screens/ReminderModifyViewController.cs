using System;
using Nut.Core.Bindings.Extensions;
using NutApp.Core.Screens.Models;

namespace NutApp.iOS.Screens
{
    public partial class ReminderModifyViewController : BaseViewController<ReminderModifyViewModel>
    {
        public override string Title => ViewModel.Id == Guid.Empty ? "New todo stuff" : ViewModel.Title;

        public ReminderModifyViewController() : base(nameof(ReminderModifyViewController))
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var bindingSet = this.CreateBindingSet<ReminderModifyViewModel>();
            bindingSet.BindText(titleTextField).To(x => x.Title).TwoWay();
            bindingSet.BindText(contentTextField).To(x => x.Content).TwoWay();
            bindingSet.BindValidation(titleTextView).To(x => x.TitleError);
            bindingSet.BindTap(saveButton).To(x => x.SaveCommand);
            bindingSet.BindTap(deleteButton).To(x => x.DeleteCommand);
            bindingSet.BindVisibility(deleteButton).To(x => x.Id);
            bindingSet.Apply();
        }
    }
}
