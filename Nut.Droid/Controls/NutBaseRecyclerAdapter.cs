using System;
using System.Collections;
using Android.Support.V7.Widget;
using Android.Views;
using Nut.Core.Bindings.Commands;

namespace Nut.Droid.Controls
{
    public abstract class NutBaseRecyclerAdapter : RecyclerView.Adapter, INutAdapter
    {
        public abstract IEnumerable Items { get; set; }

        public INutCommand ItemSelectCommand { get; set; }

        protected virtual View CreateItemView(ViewGroup parent, int resourceId)
        {
            var inflater = LayoutInflater.From(parent.Context);
            return inflater.Inflate(resourceId, parent, false);
        }

        protected virtual INutCollectionItem CreateItemContainer(Type itemType, View itemView, int viewType)
        {
            var item = Activator.CreateInstance(itemType, itemView) as INutCollectionItem;
            if (item == null)
            {
                throw new ArgumentException("Collection item must be convertible to `{0}`", nameof(INutCollectionItem));
            }

            return CreateSelectableCollectionItem(item, viewType);
        }

        protected virtual INutCollectionItem CreateSelectableCollectionItem(INutCollectionItem item, int viewType)
        {
            if (ItemSelectCommand != null)
            {
                item.ItemView.Click += (sender, args) =>
                {
                    var source = item.DataSource;
                    ItemSelectCommand.Execute(source);
                };
            }

            return item;
        }

        public virtual void Destroy()
        {
        }
    }
}