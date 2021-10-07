using Android.Graphics;
using Nut.Core.Platform;

namespace Nut.Droid.Extensions
{
    public static class NutDroidColorExtensions
    {
        public static Color ToNative(this NutColor source)
        {
            return new Color(source.R, source.G, source.B, source.A);
        }
    }
}