using System;
using System.Collections;
using Android.Content;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Util;

namespace Nut.Droid.Controls
{
    [Register("nut.droid.controls.nutrecyclerview")]
    public class NutRecyclerView : RecyclerView, INutCollectionView
    {
        private bool configured;

        public NutRecyclerView(Context context) : base(context)
        {
            Configure(context);
        }

        public NutRecyclerView(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            Configure(context);
        }

        public NutRecyclerView(Context context, IAttributeSet attrs, int defStyle) : base(context, attrs, defStyle)
        {
            Configure(context);
        }

        public NutRecyclerView(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }

        public new INutAdapter Adapter
        {
            get { return GetAdapter() as INutAdapter; }
            set
            {
                var existing = Adapter;
                if (existing == value)
                {
                    return;
                }

                if (existing?.Items != null)
                {
                    value.Items = existing.Items;
                }

                SetAdapter(value as Adapter);
            }
        }

        private void Configure(Context context)
        {
            if (configured)
            {
                return;
            }

            ConfigureLayoutManager(context);

            configured = true;
        }

        protected virtual void ConfigureLayoutManager(Context context)
        {
            var layoutManager = new LinearLayoutManager(context);
            SetLayoutManager(layoutManager);
        }
    }
}