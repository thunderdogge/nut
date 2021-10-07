using Android.Widget;
using Nut.Core.Bindings;
using Nut.Core.Extensions;

namespace Nut.Droid.Bindings
{
    public class NutDroidCheckedTargetBinding : NutTargetBinding
    {
        private readonly CheckBox target;

        public NutDroidCheckedTargetBinding(CheckBox target)
        {
            this.target = target;
        }

        public override void SetValue(object value)
        {
            var isChecked = NutObjectExtensions.IsTrueValue(value);
            if (target.Checked == isChecked)
            {
                return;
            }

            target.Checked = isChecked;
        }

        public override void SubscribeToEvents()
        {
            base.SubscribeToEvents();

            target.CheckedChange += HandleCheckedChange;
        }

        public override void UnsubscribeFromEvents()
        {
            base.UnsubscribeFromEvents();

            target.CheckedChange -= HandleCheckedChange;
        }

        private void HandleCheckedChange(object s, CompoundButton.CheckedChangeEventArgs args)
        {
            FireValueChanged(args.IsChecked);
        }
    }
}