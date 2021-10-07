using Android.Support.Design.Widget;
using Android.Views;
using Android.Widget;
using Nut.Core.Bindings;

namespace Nut.Droid.Bindings
{
    public class NutDroidValidationTargetBinding : NutTargetBinding
    {
        private readonly EditText editText;
        private readonly TextInputLayout editTextLayout;

        public NutDroidValidationTargetBinding(View target)
        {
            editText = target as EditText;
            if (editText == null)
            {
                return;
            }

            editTextLayout = editText.Parent.Parent as TextInputLayout;
        }

        public override void SetValue(object value)
        {
            var validation = value?.ToString();

            if (editTextLayout != null)
            {
                editTextLayout.Error = validation;
                editTextLayout.ErrorEnabled = !string.IsNullOrEmpty(validation);
                return;
            }

            if (editText != null)
            {
                editText.Error = validation;
            }
        }
    }
}