using System;
using System.Collections;
using System.Collections.Generic;
using Android.Support.V7.Widget;
using Android.Views;
using Nut.Core.Platform;

namespace Nut.Droid.Controls
{
    public class NutRecyclerGroupedAdapter<TItemType> : NutBaseRecyclerAdapter
    {
        private readonly Type itemType;
        private readonly int templateId;
        private readonly int separatorId;
        private readonly List<ViewItem> itemsList;
        private IEnumerable<INutGroup> groups;
        private int itemsCount;

        public NutRecyclerGroupedAdapter(int templateId, int separatorId)
        {
            this.itemType = typeof(TItemType);
            this.templateId = templateId;
            this.separatorId = separatorId;

            itemsList = new List<ViewItem>();
        }

        public override IEnumerable Items
        {
            get { return groups; }
            set
            {
                groups = (IEnumerable<INutGroup>) value;
                itemsList.Clear();

                foreach (var group in groups)
                {
                    itemsList.Add(new ViewItem
                    {
                        Value = group,
                        Type = NutRecyclerViewType.Separator
                    });
                    foreach (var item in group.Items)
                    {
                        itemsList.Add(new ViewItem
                        {
                            Value = item,
                            Type = NutRecyclerViewType.Item
                        });
                    }
                }

                itemsCount = itemsList.Count;

                NotifyDataSetChanged();
            }
        }

        public override int ItemCount => itemsCount;

        public override int GetItemViewType(int position)
        {
            return (int) itemsList[position].Type;
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            var viewHolder = holder as NutRecyclerViewHolder;
            if (viewHolder == null)
            {
                return;
            }

            var itemModel = itemsList[position].Value;
            viewHolder.CollectionItem.DataSource = itemModel;
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var recyclerViewType = (NutRecyclerViewType) viewType;
            switch (recyclerViewType)
            {
                case NutRecyclerViewType.Item:
                {
                    var itemView = CreateItemView(parent, templateId);
                    var itemWrap = CreateItemContainer(itemType, itemView, viewType);
                    return new NutRecyclerViewHolder(itemWrap);
                }
                case NutRecyclerViewType.Separator:
                {
                    var itemView = CreateItemView(parent, separatorId);
                    var itemWrap = new NutGroupCollectionItem(itemView);
                    return new NutRecyclerViewHolder(itemWrap);
                }
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public class ViewItem
        {
            public NutRecyclerViewType Type { get; set; }
            public object Value { get; set; }
        }
    }
}