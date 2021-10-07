using System;
using Foundation;
using Nut.Core.Bindings.Extensions;
using Nut.iOS.Controls;
using NutApp.Core.Screens.Models;
using UIKit;

namespace NutApp.iOS.Screens
{
    public partial class ReminderItemCell : NutTableCell
    {
        public static readonly NSString Key = new NSString("ReminderItemCell");
        public static readonly UINib Nib;

        static ReminderItemCell()
        {
            Nib = UINib.FromName(Key, NSBundle.MainBundle);
        }

        public ReminderItemCell(IntPtr handle) : base(handle)
        {
        }

        protected override void ConfigureBindings()
        {
            var bindingSet = this.CreateBindingSet<ReminderItemViewModel>();
            bindingSet.BindText(titleLabel).To(x => x.Title);
            bindingSet.BindText(dateLabel).To(x => x.Date);
            bindingSet.BindText(contentLabel).To(x => x.Content);
            bindingSet.Apply();
        }
    }
}
