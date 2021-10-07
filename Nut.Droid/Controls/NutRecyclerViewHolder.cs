using System;
using Android.Runtime;
using Android.Support.V7.Widget;

namespace Nut.Droid.Controls
{
    public class NutRecyclerViewHolder : RecyclerView.ViewHolder
    {
        public NutRecyclerViewHolder(INutCollectionItem collectionItem) : base(collectionItem.ItemView)
        {
            CollectionItem = collectionItem;
        }

        public NutRecyclerViewHolder(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }

        public INutCollectionItem CollectionItem { get; }
    }
}