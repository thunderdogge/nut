using System;
using System.Collections;
using Android.Support.V7.Widget;
using Android.Views;
using Nut.Core.Extensions;

namespace Nut.Droid.Controls
{
    public class NutRecyclerAdapter<TItemType> : NutRecyclerAdapter
    {
        private readonly Type itemType;
        private readonly int itemResourceId;

        public NutRecyclerAdapter(int itemResourceId)
        {
            this.itemType = typeof(TItemType);
            this.itemResourceId = itemResourceId;
        }

        protected override View CreateCollectionItemView(ViewGroup parent, int viewType)
        {
            return CreateItemView(parent, itemResourceId);
        }

        protected override INutCollectionItem CreateCollectionItemContainer(View itemView, int viewType)
        {
            return CreateItemContainer(itemType, itemView, viewType);
        }
    }

    public abstract class NutRecyclerAdapter : NutBaseRecyclerAdapter
    {
        private int itemsCount;
        private IEnumerable items;

        public override IEnumerable Items
        {
            get { return items; }
            set
            {
                items = value;
                itemsCount = value.Count();

                NotifyDataSetChanged();
            }
        }

        public override int ItemCount => itemsCount;

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            var viewHolder = holder as NutRecyclerViewHolder;
            if (viewHolder == null)
            {
                return;
            }

            var itemModel = Items.ElementAt(position);
            viewHolder.CollectionItem.DataSource = itemModel;
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var itemView = CreateCollectionItemView(parent, viewType);
            var itemWrap = CreateCollectionItemContainer(itemView, viewType);
            return new NutRecyclerViewHolder(itemWrap);
        }

        protected abstract View CreateCollectionItemView(ViewGroup parent, int viewType);

        protected abstract INutCollectionItem CreateCollectionItemContainer(View itemView, int viewType);
    }
}