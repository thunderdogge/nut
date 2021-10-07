using Android.Graphics.Drawables;
using Android.Widget;

namespace Nut.Droid.Extensions
{
    public static class NutDroidImageViewExtensions
    {
        public static void RecycleBitmap(this ImageView source)
        {
            if (source == null)
            {
                return;
            }

            var drawable = source.Drawable as BitmapDrawable;
            if (drawable != null)
            {
                if (drawable.Bitmap != null)
                {
                    drawable.Bitmap.Recycle();
                }
            }
        }
    }
}