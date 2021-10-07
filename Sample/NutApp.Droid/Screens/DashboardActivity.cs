using Android.App;
using Android.OS;
using Android.Views;
using Nut.Core.Bindings.Extensions;
using Nut.Droid.Controls;
using NutApp.Core.Screens.Models;

namespace NutApp.Droid.Screens
{
    [Activity(Theme = "@style/AppTheme")]
    public class DashboardActivity : BaseActivity<DashboardViewModel>
    {
        protected override int LayoutView => Resource.Layout.dashboard;
        protected override string LayoutTitle => "Some todo stuff";

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            var addItem = FindViewById(Resource.Id.addItem);
            var contentList = FindViewById<NutRecyclerView>(Resource.Id.contentList);
            var contentEmpty = FindViewById(Resource.Id.contentEmpty);
            contentList.Adapter = new NutRecyclerAdapter<ReminderListCell>(Resource.Layout.reminder_item);

            var bindingSet = this.CreateBindingSet<DashboardViewModel>();
            bindingSet.BindTap(addItem).To(x => x.ItemAddCommand);
            bindingSet.BindSource(contentList.Adapter).To(x => x.Items);
            bindingSet.BindSourceSelect(contentList.Adapter).To(x => x.ItemSelectCommand);
            bindingSet.BindVisibility(contentEmpty).To(x => x.IsEmpty);
            bindingSet.BindInvisibility(contentList).To(x => x.IsEmpty);
            bindingSet.Apply();
        }

        protected override void ConfigureToolbar()
        {
            base.ConfigureToolbar();
            SupportActionBar?.SetDisplayHomeAsUpEnabled(false);
        }
    }

    public class ReminderListCell : NutCollectionItem
    {
        private readonly View date;
        private readonly View title;
        private readonly View content;

        public ReminderListCell(View itemView) : base(itemView)
        {
            date = itemView.FindViewById(Resource.Id.itemDate);
            title = itemView.FindViewById(Resource.Id.itemTitle);
            content = itemView.FindViewById(Resource.Id.itemContent);
        }

        protected override void ConfigureBindings()
        {
            var bindingSet = this.CreateBindingSet<ReminderItemViewModel>();
            bindingSet.BindText(date).To(x => x.Date);
            bindingSet.BindText(title).To(x => x.Title);
            bindingSet.BindText(content).To(x => x.Content);
            bindingSet.BindVisibility(content).To(x => x.Content);
            bindingSet.Apply();
        }
    }
}
