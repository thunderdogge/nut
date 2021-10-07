using System.Collections;
using Android.Views;
using Android.Widget;
using Java.Lang;
using Nut.Core.Bindings.Commands;
using Nut.Core.Extensions;

namespace Nut.Droid.Controls
{
    public abstract class NutBaseAdapter : BaseAdapter, INutAdapter
    {
        private int itemsCount;
        private IEnumerable items;

        public virtual IEnumerable Items
        {
            get { return items; }
            set
            {
                items = value;
                itemsCount = value.Count();

                NotifyDataSetChanged();
            }
        }

        public virtual INutCommand ItemSelectCommand { get; set; }

        public override int Count => itemsCount;

        public override Object GetItem(int position)
        {
            return Items.ElementAt(position) as Object;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public virtual object GetRawItem(int position)
        {
            return Items.ElementAt(position);
        }

        protected virtual View CreateItemView(ViewGroup parent, int position, int templateId)
        {
            return LayoutInflater.From(parent.Context).Inflate(templateId, parent, false);
        }

        public virtual void Destroy()
        {
        }
    }
}