using System;
using System.Collections;
using System.Collections.Generic;
using Android.Views;
using Android.Widget;

namespace Nut.Droid.Controls
{
    public class NutListAdapter<TItemType> : NutBaseAdapter, INutListAdapter
    {
        private readonly Type itemType;
        private readonly int itemResourceId;
        private readonly Dictionary<int, INutCollectionItem> viewCache = new Dictionary<int, INutCollectionItem>();

        public NutListAdapter(int itemResourceId)
        {
            this.itemType = typeof(TItemType);
            this.itemResourceId = itemResourceId;
        }

        public override IEnumerable Items
        {
            get { return base.Items; }
            set
            {
                if (base.Items != null)
                {
                    viewCache.Clear();
                }

                base.Items = value;
            }
        }

        public event Action<INutListAdapter> DataSetChanged = delegate {};

        public void OnItemClick(AdapterView parent, View view, int position, long id)
        {
            var command = ItemSelectCommand;
            if (id < 0 || command == null)
            {
                return;
            }

            var realPosition = Math.Min((int)id, position);
            var itemModel = GetRawItem(realPosition);
            command.Execute(itemModel);
        }

        public virtual View GetView(int position, View parentView)
        {
            return GetView(position, parentView, parentView as ViewGroup);
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            return GetView(position, convertView, parent, itemResourceId);
        }

        protected virtual View GetView(int position, View convertView, ViewGroup parent, int templateId)
        {
            if (Items == null)
            {
                throw new Exception("ListView items property is null");
            }

            if (viewCache.ContainsKey(position))
            {
                return viewCache[position].ItemView;
            }

            var itemSource = GetRawItem(position);
            var inflatedView = CreateItemView(parent, position, templateId);
            var itemElement = CreateCollectionItem(inflatedView);

            itemElement.DataSource = itemSource;

            viewCache[position] = itemElement;

            return itemElement.ItemView;
        }

        protected virtual INutCollectionItem CreateCollectionItem(View inflatedView)
        {
            return (INutCollectionItem) Activator.CreateInstance(itemType, inflatedView);
        }

        public override void NotifyDataSetChanged()
        {
            base.NotifyDataSetChanged();
            DataSetChanged.Invoke(this);
        }

        public override void Destroy()
        {
            if (viewCache != null)
            {
                foreach (var item in viewCache.Values)
                {
                    item.BindingContext.UnregisterBindings();
                }
            }
        }
    }
}