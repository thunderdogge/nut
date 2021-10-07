using System;
using Android.App;
using Android.OS;
using Nut.Core.Bindings.Extensions;
using NutApp.Core.Screens.Models;

namespace NutApp.Droid.Screens
{
    [Activity(Theme = "@style/AppTheme")]
    public class ReminderModifyActivity : BaseActivity<ReminderModifyViewModel>
    {
        protected override int LayoutView => Resource.Layout.reminder_modify;
        protected override string LayoutTitle => ViewModel.Id == Guid.Empty ? "New todo stuff" : ViewModel.Title;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            var title = FindViewById(Resource.Id.itemTitle);
            var content = FindViewById(Resource.Id.itemContent);
            var save = FindViewById(Resource.Id.saveItem);
            var delete = FindViewById(Resource.Id.deleteItem);

            var bindingSet = this.CreateBindingSet<ReminderModifyViewModel>();
            bindingSet.BindText(title).To(x => x.Title).TwoWay();
            bindingSet.BindValidation(title).To(x => x.TitleError);
            bindingSet.BindText(content).To(x => x.Content).TwoWay();
            bindingSet.BindTap(save).To(x => x.SaveCommand);
            bindingSet.BindTap(delete).To(x => x.DeleteCommand);
            bindingSet.BindVisibility(delete).To(x => x.Id);
            bindingSet.Apply();
        }
    }
}