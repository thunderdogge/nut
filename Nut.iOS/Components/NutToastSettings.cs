using System;
using CoreGraphics;

namespace Nut.iOS.Components
{
    public class NutToastSettings : ICloneable
    {
        public nint Duration { get; set; }
        public NutToastGravity Gravity { get; set; }
        public CGPoint Position { get; set; }
        public nfloat FontSize { get; set; }
        public bool UseShadow { get; set; }
        public nfloat CornerRadius { get; set; }
        public nfloat BgRed { get; set; }
        public nfloat BgGreen { get; set; }
        public nfloat BgBlue { get; set; }
        public nfloat BgAlpha { get; set; }
        public nint OffsetLeft { get; set; }
        public nint OffsetTop { get; set; }
        public bool PositionIsSet { get; set; }

        public static NutToastSettings SharedSettings { get; set; }

        public static void MakeNewSetting()
        {
            SharedSettings = null;
        }

        public static NutToastSettings GetSharedSettings()
        {
            if (SharedSettings != null)
            {
                return SharedSettings;
            }

            SharedSettings = new NutToastSettings();
            SharedSettings.Gravity = NutToastGravity.Bottom;
            SharedSettings.Duration = 1000;
            SharedSettings.FontSize = 16f;
            SharedSettings.UseShadow = false;
            SharedSettings.CornerRadius = 3f;
            SharedSettings.BgRed = 0;
            SharedSettings.BgGreen = 0;
            SharedSettings.BgBlue = 0;
            SharedSettings.BgAlpha = 0.7f;
            SharedSettings.OffsetLeft = 0;
            SharedSettings.OffsetTop = 0;

            return SharedSettings;
        }

        public object Clone()
        {
            return new NutToastSettings
            {
                Gravity = Gravity,
                Duration = Duration,
                Position = Position,
                FontSize = FontSize,
                UseShadow = UseShadow,
                CornerRadius = CornerRadius,
                BgRed = BgRed,
                BgGreen = BgGreen,
                BgBlue = BgBlue,
                BgAlpha = BgAlpha,
                OffsetLeft = OffsetLeft,
                OffsetTop = OffsetTop
            };
        }
    }
}