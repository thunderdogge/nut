using System;
using CoreGraphics;
using Foundation;
using UIKit;

namespace Nut.iOS.Components
{
    public class NutToast
    {
        private static NutToastSettings settings;
        private nfloat kComponentPadding = 10;
        private string text;
        private UIView view;

        public NutToast()
        {
        }

        public NutToast(string itext)
        {
            text = itext;
        }

        public void Show()
        {
            var size =
                new NSAttributedString(this.text, UIFont.SystemFontOfSize(GetSettings().FontSize), (UIColor)null,
                    (UIColor)null, (UIColor)null, (NSParagraphStyle)null, NSLigatureType.Default, 0.0f,
                    NSUnderlineStyle.None, (NSShadow)null, 0.0f, NSUnderlineStyle.None).GetBoundingRect(
                        new CGSize((nfloat)260, (nfloat)60), NSStringDrawingOptions.UsesLineFragmentOrigin,
                        (NSStringDrawingContext)null).Size;
            size.Width += (nfloat)5;
            UILabel uiLabel =
                new UILabel(new CGRect((nfloat)0, (nfloat)0, size.Width + this.kComponentPadding,
                    size.Height + this.kComponentPadding));
            uiLabel.BackgroundColor = UIColor.Clear;
            uiLabel.TextColor = UIColor.White;
            uiLabel.TextAlignment = UITextAlignment.Center;
            uiLabel.Text = this.text;
            uiLabel.Lines = (nint)0;
            uiLabel.Font = UIFont.SystemFontOfSize(GetSettings().FontSize);
            if (GetSettings().UseShadow)
            {
                uiLabel.ShadowColor = UIColor.DarkGray;
                uiLabel.ShadowOffset = new CGSize((nfloat)1, (nfloat)1);
            }
            UIButton uiButton = new UIButton(UIButtonType.Custom);

            uiButton.Frame = new CGRect((nfloat)0, (nfloat)0, size.Width + this.kComponentPadding * (nfloat)2,
                size.Height + this.kComponentPadding * (nfloat)2);
            uiLabel.Center = new CGPoint(uiButton.Frame.Size.Width / (nfloat)2, uiButton.Frame.Size.Height / (nfloat)2);

            CGRect frame = uiLabel.Frame;
            frame.X = (nfloat)Math.Ceiling((double)frame.X);
            frame.Y = (nfloat)Math.Ceiling((double)frame.Y);
            uiLabel.Frame = frame;

            uiButton.AddSubview((UIView)uiLabel);

            uiButton.BackgroundColor = UIColor.FromRGBA(GetSettings().BgRed, GetSettings().BgGreen,
                GetSettings().BgBlue, GetSettings().BgAlpha);
            uiButton.Layer.CornerRadius = GetSettings().CornerRadius;
            UIWindow uiWindow = UIApplication.SharedApplication.Windows[0];
            CGPoint cgPoint = CGPoint.Empty;
            UIInterfaceOrientation statusBarOrientation = UIApplication.SharedApplication.StatusBarOrientation;
            switch (GetSettings().Gravity)
            {
                case NutToastGravity.Top:
                    cgPoint = new CGPoint(uiWindow.Frame.Size.Width / (nfloat)2, (nfloat)45);
                    break;
                case NutToastGravity.Bottom:
                    cgPoint = new CGPoint(uiWindow.Frame.Size.Width / (nfloat)2, uiWindow.Frame.Size.Height - (nfloat)45);
                    break;
                case NutToastGravity.Center:
                    cgPoint = new CGPoint(uiWindow.Frame.Size.Width / (nfloat)2, uiWindow.Frame.Size.Height / (nfloat)2);
                    break;
            }
            cgPoint = new CGPoint(cgPoint.X + (nfloat)GetSettings().OffsetLeft,
                cgPoint.Y + (nfloat)GetSettings().OffsetTop);
            uiButton.Center = cgPoint;
            uiButton.Frame = uiButton.Frame.Integral();

            NSRunLoop.Main.AddTimer(
                NSTimer.CreateTimer(TimeSpan.FromSeconds((double)GetSettings().Duration / 1000.0),
                    (Action<NSTimer>)(timer => this.HideToast())), NSRunLoopMode.Default);
            uiButton.Tag = (nint)6984678L;

            UIView uiView = uiWindow.ViewWithTag((nint)6984678L);
            if (uiView != null)
            {
                uiView.RemoveFromSuperview();
            }
            uiButton.Alpha = 0;

            uiWindow.AddSubview(uiButton);

            UIView.BeginAnimations(null, IntPtr.Zero);
            uiButton.Alpha = 1;

            UIView.CommitAnimations();
            this.view = (UIView)uiButton;
            uiButton.AddTarget(new EventHandler(this.HideToastEventHandler), UIControlEvent.TouchDown);
            NutToastSettings.SharedSettings = (NutToastSettings)null;
        }

        private void HideToastEventHandler(object sender, EventArgs e)
        {
            HideToast();
        }

        private void HideToast()
        {
            UIView.BeginAnimations((string)null, IntPtr.Zero);
            this.view.Alpha = (nfloat)0;
            UIView.CommitAnimations();

            NSRunLoop.Main.AddTimer(
                NSTimer.CreateTimer(TimeSpan.FromSeconds(0.5), (Action<NSTimer>)(timer => this.HideToast())),
                NSRunLoopMode.Default);
        }

        public NutToast SetDuration(nint duration)
        {
            GetSettings().Duration = duration;
            return this;
        }

        public NutToast SetGravity(NutToastGravity gravity, nint left, nint top)
        {
            GetSettings().Gravity = gravity;
            GetSettings().OffsetLeft = left;
            GetSettings().OffsetTop = top;
            return this;
        }

        public NutToast SetGravity(NutToastGravity gravity)
        {
            GetSettings().Gravity = gravity;
            return this;
        }

        public NutToast SetPosition(CGPoint position)
        {
            GetSettings().Position = position;
            return this;
        }

        public NutToast SetFontSize(nfloat fontSize)
        {
            GetSettings().FontSize = fontSize;
            return this;
        }

        public NutToast SetUseShadow(bool useShadow)
        {
            GetSettings().UseShadow = useShadow;
            return this;
        }

        public NutToast SetCornerRadius(nfloat cornerRadius)
        {
            GetSettings().CornerRadius = cornerRadius;
            return this;
        }

        public NutToast SetBgRed(nfloat bgRed)
        {
            GetSettings().BgRed = bgRed;
            return this;
        }

        public NutToast SetBgGreen(nfloat bgGreen)
        {
            GetSettings().BgGreen = bgGreen;
            return this;
        }

        public NutToast SetBgBlue(nfloat bgBlue)
        {
            GetSettings().BgBlue = bgBlue;
            return this;
        }

        public NutToast SetBgAlpha(nfloat bgAlpha)
        {
            GetSettings().BgAlpha = bgAlpha;
            return this;
        }

        public static NutToast MakeText(string text)
        {
            var toast = new NutToast(text);
            toast.MakeNewSetting();

            return toast;
        }

        public static NutToast MakeText(string text, nint duration)
        {
            return MakeText(text).SetDuration(duration);
        }

        public NutToastSettings GetSettings()
        {
            return NutToastSettings.GetSharedSettings();
        }

        public void MakeNewSetting()
        {
            NutToastSettings.MakeNewSetting();
        }
    }
}