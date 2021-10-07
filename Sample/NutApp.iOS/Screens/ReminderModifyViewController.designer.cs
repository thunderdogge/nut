// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace NutApp.iOS.Screens
{
	[Register ("ReminderModifyViewController")]
	partial class ReminderModifyViewController
	{
		[Outlet]
		UIKit.UITextField contentTextField { get; set; }

		[Outlet]
		NutApp.iOS.Controls.UITextFieldView contentTextView { get; set; }

		[Outlet]
		UIKit.UIButton deleteButton { get; set; }

		[Outlet]
		UIKit.UIButton saveButton { get; set; }

		[Outlet]
		UIKit.UITextField titleTextField { get; set; }

		[Outlet]
		NutApp.iOS.Controls.UITextFieldView titleTextView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (titleTextView != null) {
				titleTextView.Dispose ();
				titleTextView = null;
			}

			if (titleTextField != null) {
				titleTextField.Dispose ();
				titleTextField = null;
			}

			if (contentTextView != null) {
				contentTextView.Dispose ();
				contentTextView = null;
			}

			if (contentTextField != null) {
				contentTextField.Dispose ();
				contentTextField = null;
			}

			if (saveButton != null) {
				saveButton.Dispose ();
				saveButton = null;
			}

			if (deleteButton != null) {
				deleteButton.Dispose ();
				deleteButton = null;
			}
		}
	}
}
