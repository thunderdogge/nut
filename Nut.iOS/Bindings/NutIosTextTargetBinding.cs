using System;
using Nut.Core.Bindings;
using UIKit;

namespace Nut.iOS.Bindings
{
    public class NutIosTextTargetBinding : NutTargetBinding
    {
        private readonly UIView target;

        public NutIosTextTargetBinding(UIView target)
        {
            this.target = target;
        }

        public override void SetValue(object value)
        {
            var targetValue = value?.ToString() ?? string.Empty;

            var uiLabel = target as UILabel;
            if (uiLabel != null)
            {
                var currentValue = uiLabel.Text;
                if (currentValue != targetValue)
                {
                    uiLabel.Text = targetValue;
                }

                return;
            }

            var uiTextField = target as UITextField;
            if (uiTextField != null)
            {
                var currentValue = uiTextField.Text;
                if (currentValue != targetValue)
                {
                    uiTextField.Text = targetValue;
                }

                return;
            }

            var uiButton = target as UIButton;
            if (uiButton != null)
            {
                var currentValue = uiButton.Title(UIControlState.Normal);
                if (currentValue != targetValue)
                {
                    uiButton.SetTitle(targetValue, UIControlState.Normal);
                }

                return;
            }

            var uiTextView = target as UITextView;
            if (uiTextView != null)
            {
                var currentValue = uiTextView.Text;
                if (currentValue != targetValue)
                {
                    uiTextView.Text = targetValue;
                }
            }
        }

        public override void SubscribeToEvents()
        {
            base.SubscribeToEvents();

            var uiTextField = target as UITextField;
            if (uiTextField != null)
            {
                uiTextField.EditingChanged += HandleTextChange;
                return;
            }

            var uiTextView = target as UITextView;
            if (uiTextView != null)
            {
                uiTextView.Changed += HandleTextChange;
            }
        }

        public override void UnsubscribeFromEvents()
        {
            base.UnsubscribeFromEvents();

            var uiTextField = target as UITextField;
            if (uiTextField != null)
            {
                uiTextField.EditingChanged -= HandleTextChange;
                return;
            }

            var uiTextView = target as UITextView;
            if (uiTextView != null)
            {
                uiTextView.Changed -= HandleTextChange;
            }
        }

        private void HandleTextChange(object sender, EventArgs e)
        {
            var uiTextField = target as UITextField;
            if (uiTextField != null)
            {
                FireValueChanged(uiTextField.Text);
                return;
            }

            var uiTextView = target as UITextView;
            if (uiTextView != null)
            {
                FireValueChanged(uiTextView.Text);
            }
        }
    }
}