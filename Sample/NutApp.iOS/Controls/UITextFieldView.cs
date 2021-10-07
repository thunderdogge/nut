using System;
using System.Linq;
using Foundation;
using UIKit;

namespace NutApp.iOS.Controls
{
    [Register("UITextFieldView")]
    public sealed class UITextFieldView : UIView
    {
        private readonly UIView borderView;
        private readonly UILabel validationText;
        private string validation;

        public UITextFieldView(IntPtr handle) : base(handle)
        {
            if (Subviews.Length != 1)
            {
                throw new Exception("UITextFieldView can contain only one direct child");
            }

            var editText = Subviews.OfType<UITextField>().FirstOrDefault();
            if (editText == null)
            {
                throw new Exception("Cannot find child view or type UITextField");
            }

            borderView = new UIView
            {
                BackgroundColor = UIColor.LightGray,
                TranslatesAutoresizingMaskIntoConstraints = false
            };

            validationText = new UILabel
            {
                Font = UIFont.SystemFontOfSize(14),
                TextColor = UIColor.Red,
                TranslatesAutoresizingMaskIntoConstraints = false
            };

            var bottomConstraint = Constraints.FirstOrDefault(x => x.FirstAttribute == NSLayoutAttribute.Bottom);
            if (bottomConstraint != null)
            {
                RemoveConstraint(bottomConstraint);
            }

            AddConstraints(new[]
            {
                NSLayoutConstraint.Create(this, NSLayoutAttribute.Leading, NSLayoutRelation.Equal, borderView, NSLayoutAttribute.Leading, 1, 0),
                NSLayoutConstraint.Create(this, NSLayoutAttribute.Trailing, NSLayoutRelation.Equal, borderView, NSLayoutAttribute.Trailing, 1, 0),
                NSLayoutConstraint.Create(borderView, NSLayoutAttribute.Top, NSLayoutRelation.Equal, editText, NSLayoutAttribute.Bottom, 1, 5),
                NSLayoutConstraint.Create(borderView, NSLayoutAttribute.Height, NSLayoutRelation.Equal, null, NSLayoutAttribute.NoAttribute, 1, 1),
                NSLayoutConstraint.Create(this, NSLayoutAttribute.Leading, NSLayoutRelation.Equal, validationText, NSLayoutAttribute.Leading, 1, 0),
                NSLayoutConstraint.Create(this, NSLayoutAttribute.Trailing, NSLayoutRelation.Equal, validationText, NSLayoutAttribute.Trailing, 1, 0),
                NSLayoutConstraint.Create(this, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, validationText, NSLayoutAttribute.Bottom, 1, 0),
                NSLayoutConstraint.Create(validationText, NSLayoutAttribute.Top, NSLayoutRelation.Equal, borderView, NSLayoutAttribute.Bottom, 1, 5)
            });

            AddSubview(borderView);
            AddSubview(validationText);
        }

        public string Validation
        {
            get { return validation; }
            set
            {
                var isEmpty = string.IsNullOrEmpty(value);
                borderView.BackgroundColor = isEmpty ? UIColor.Gray : UIColor.Red;
                validationText.Text = value;
                validationText.Hidden = isEmpty;

                validation = value;
            }
        }
    }
}