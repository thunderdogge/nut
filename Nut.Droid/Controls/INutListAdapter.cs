using System;
using Android.Views;
using Android.Widget;

namespace Nut.Droid.Controls
{
    public interface INutListAdapter : INutAdapter, IListAdapter, ISpinnerAdapter, AdapterView.IOnItemClickListener
    {
        object GetRawItem(int position);
        View GetView(int position, View parentView);
        event Action<INutListAdapter> DataSetChanged;
    }
}