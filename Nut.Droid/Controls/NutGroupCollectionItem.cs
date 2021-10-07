using Android.Views;
using Android.Widget;
using Nut.Core.Bindings.Extensions;
using Nut.Core.Platform;
using Nut.Droid.Extensions;

namespace Nut.Droid.Controls
{
    public class NutGroupCollectionItem : NutCollectionItem
    {
        private readonly TextView textView;

        public NutGroupCollectionItem(View itemView) : base(itemView)
        {
            textView = itemView.FindFirstOrDefault<TextView>();
        }

        protected override void ConfigureBindings()
        {
            var bindingSet = this.CreateBindingSet<INutGroup>();
            bindingSet.BindText(textView).To(x => x.Key);
            bindingSet.Apply();
        }
    }
}