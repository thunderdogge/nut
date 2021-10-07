using System;
using System.Collections;
using Foundation;
using Nut.Core.Bindings;
using Nut.Core.Bindings.Commands;
using UIKit;

namespace Nut.iOS.Controls
{
    public abstract class NutTableViewSourceBase : UITableViewSource, INutCollectionSource
    {
        protected const int DefaultEstimatedRowHeight = 70;

        protected NutTableViewSourceBase(UITableView table, string nibName)
        {
            Table = table;
            NibName = nibName;

            var nib = UINib.FromName(nibName, NSBundle.MainBundle);
            Table.RegisterNibForCellReuse(nib, nibName);
            Table.RowHeight = UITableView.AutomaticDimension;
            Table.EstimatedRowHeight = DefaultEstimatedRowHeight;
            Table.Source = this;
        }

        public string NibName { get; set; }

        public UITableView Table { get; set; }

        public abstract IEnumerable Items { get; set; }

        public INutCommand ItemSelectCommand { get; set; }

        public bool DeselectAutomatically { get; set; } = true;

        protected abstract object GetItemAt(NSIndexPath indexPath);

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var reusableCell = tableView.DequeueReusableCell(NibName, indexPath);
            return GetBindableCell(reusableCell, indexPath);
        }

        protected virtual UITableViewCell GetBindableCell(UITableViewCell tableCell, NSIndexPath indexPath)
        {
            var bindableCell = tableCell as INutTableCell;
            if (bindableCell == null)
            {
                throw new ArgumentException("Cell must be convertible to `{0}`", nameof(INutTableCell));
            }

            var itemSource = GetItemAt(indexPath);
            bindableCell.DataSource = itemSource;

            return tableCell;
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            if (DeselectAutomatically)
            {
                tableView.DeselectRow(indexPath, true);
            }

            var selectCommand = ItemSelectCommand;
            if (selectCommand == null)
            {
                return;
            }

            var itemSource = GetItemAt(indexPath);
            if (selectCommand.CanExecute(itemSource))
            {
                selectCommand.Execute(itemSource);
            }
        }

        public override nint NumberOfSections(UITableView tableView)
        {
            return 1;
        }
    }
}