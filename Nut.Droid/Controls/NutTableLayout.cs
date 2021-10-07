using System;
using Android.Content;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace Nut.Droid.Controls
{
    [Register("nut.droid.controls.nuttablelayout")]
    public class NutTableLayout : TableLayout, INutCollectionView
    {
        private INutAdapter adapter;

        public NutTableLayout(Context context, IAttributeSet attrs) : base(context, attrs)
        {
        }

        public NutTableLayout(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }

        public INutAdapter Adapter
        {
            get { return adapter; }
            set
            {
                var newAdapter = value as INutListAdapter;
                if (newAdapter == null)
                {
                    throw new ArgumentException("Adapter must be convertible to `{0}`", nameof(INutListAdapter));
                }

                var existingAdapter = adapter as INutListAdapter;
                if (existingAdapter == newAdapter)
                {
                    return;
                }

                if (existingAdapter != null)
                {
                    existingAdapter.DataSetChanged -= HandleDataSetChanged;

                    if (existingAdapter.Items != null)
                    {
                        newAdapter.Items = existingAdapter.Items;
                    }
                }

                newAdapter.DataSetChanged += HandleDataSetChanged;

                adapter = newAdapter;
            }
        }

        protected virtual void HandleDataSetChanged(INutListAdapter senderAdapter)
        {
            if (ChildCount > 0)
            {
                RemoveAllViews();
            }

            var itemsCount = senderAdapter.Count;
            for (var i = 0; i < itemsCount; i++)
            {
                var position = i;
                var adapterView = senderAdapter.GetView(position, this);

                adapterView.Click += (sender, args) =>
                {
                    senderAdapter.OnItemClick(null, sender as View, position, position);
                };

                AddView(adapterView, position);
            }
        }
    }
}