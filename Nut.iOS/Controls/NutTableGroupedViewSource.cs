using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using Nut.Core.Extensions;
using Nut.Core.Platform;
using Nut.iOS.Extensions;
using Nut.iOS.Views;
using UIKit;

namespace Nut.iOS.Controls
{
    public class NutTableGroupedViewSource : NutTableViewSourceBase
    {
        private readonly string headerNibName;
        private int groupsCount;
        private IEnumerable<INutGroup> groups;

        public NutTableGroupedViewSource(UITableView table, string nibName) : this(table, nibName, null)
        {
        }

        public NutTableGroupedViewSource(UITableView table, string nibName, string headerNibName) : base(table, nibName)
        {
            this.headerNibName = headerNibName;

            Table.EstimatedSectionHeaderHeight = DefaultEstimatedRowHeight;
        }

        public override IEnumerable Items
        {
            get { return groups; }
            set
            {
                groups = (IEnumerable<INutGroup>) value;
                groupsCount = groups.Count();

                Table.ReloadData();
            }
        }

        protected virtual INutGroup GetGroupAt(nint index)
        {
            return groups.ElementAt((int) index);
        }

        protected override object GetItemAt(NSIndexPath indexPath)
        {
            return GetGroupAt(indexPath.Section).Items.ElementAt(indexPath.Row);
        }

        public override string TitleForHeader(UITableView tableView, nint section)
        {
            return GetGroupAt(section).Key?.ToString();
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return GetGroupAt(section).Items.Count();
        }

        public override nint NumberOfSections(UITableView tableView)
        {
            return groupsCount;
        }

        public override UIView GetViewForHeader(UITableView tableView, nint section)
        {
            if (string.IsNullOrEmpty(headerNibName))
            {
                return null;
            }

            var headerView = NutIosViewLoader.FromNib(this, headerNibName);
            var labelView = headerView.FindFirstOrDefault<UILabel>();
            if (labelView != null)
            {
                labelView.Text = TitleForHeader(tableView, section);
            }

            return headerView;
        }
    }
}