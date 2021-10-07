using Nut.Core.Platform;
using UIKit;

namespace Nut.iOS.Extensions
{
    public static class NutIosColorExtensions
    {
        public static UIColor ToNative(this NutColor source)
        {
            return UIColor.FromRGBA(source.R, source.G, source.B, source.A);
        }
    }
}