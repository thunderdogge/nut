using Android.Support.Design.Widget;
using Android.Text;
using Android.Widget;
using Nut.Core.Bindings;

namespace Nut.Droid.Bindings
{
    public class NutDroidTextTargetBinding : NutTargetBinding
    {
        private readonly TextView textView;
        private readonly EditText editText;
        private readonly TextInputLayout textInputLayout;

        public NutDroidTextTargetBinding(TextView target)
        {
            textView = target;
            editText = target as EditText;
            textInputLayout = editText?.Parent?.Parent as TextInputLayout;
        }

        public override void SetValue(object value)
        {
            var targetValue = value?.ToString() ?? string.Empty;
            var currentValue = textView.Text;
            if (currentValue == targetValue)
            {
                return;
            }

            if (targetValue.Length == 0)
            {
                textView.Text = targetValue;
                return;
            }

            var hintAnimationWasEnabled = true;
            if (textInputLayout != null)
            {
                hintAnimationWasEnabled = textInputLayout.HintAnimationEnabled;
                textInputLayout.HintAnimationEnabled = false;
            }

            textView.Text = targetValue;

            if (textInputLayout != null && hintAnimationWasEnabled)
            {
                textInputLayout.HintAnimationEnabled = true;
            }

            editText?.SetSelection(targetValue.Length);
        }

        public override void SubscribeToEvents()
        {
            base.SubscribeToEvents();

            if (editText == null)
            {
                return;
            }

            editText.AfterTextChanged += HandleTextChanged;
        }

        public override void UnsubscribeFromEvents()
        {
            base.UnsubscribeFromEvents();

            if (editText == null)
            {
                return;
            }

            editText.AfterTextChanged -= HandleTextChanged;
        }

        private void HandleTextChanged(object sender, AfterTextChangedEventArgs e)
        {
            FireValueChanged(((EditText)sender).Text);
        }
    }
}