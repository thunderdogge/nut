using System;
using System.Collections;
using Foundation;
using Nut.Core.Extensions;
using Nut.iOS.Views;
using UIKit;

namespace Nut.iOS.Controls
{
    public class NutTableViewSource : NutTableViewSourceBase
    {
        private int itemsCount;
        private IEnumerable items;
        private readonly string headerNibName;

        public NutTableViewSource(UITableView table, string nibName) : base(table, nibName)
        {
        }

        public NutTableViewSource(UITableView table, string nibName, string headerNibName) : base(table, nibName)
        {
            this.headerNibName = headerNibName;

            Table.EstimatedSectionHeaderHeight = DefaultEstimatedRowHeight;
        }

        public override IEnumerable Items
        {
            get { return items; }
            set
            {
                items = value;
                itemsCount = value.Count();
                Table.ReloadData();
            }
        }

        protected override object GetItemAt(NSIndexPath indexPath)
        {
            return Items?.ElementAt(indexPath.Row);
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return itemsCount;
        }

        public override UIView GetViewForHeader(UITableView tableView, nint section)
        {
            if (string.IsNullOrEmpty(headerNibName))
            {
                return null;
            }

            return NutIosViewLoader.FromNib(this, headerNibName);
        }
    }
}