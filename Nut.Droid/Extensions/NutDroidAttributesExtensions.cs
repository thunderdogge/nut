using Android.Content;
using Android.Util;

namespace Nut.Droid.Extensions
{
    public static class NutDroidAttributesExtensions
    {
        private static int GetResourceValue(this IAttributeSet source, Context context, int[] groupId, int attrId)
        {
            var typedArray = context.ObtainStyledAttributes(source, groupId);

            try
            {
                return typedArray.GetResourceId(attrId, 0);
            }
            finally
            {
                typedArray.Recycle();
            }
        }

        private static int GetDimensionValue(this IAttributeSet source, Context context, int[] groupId, int attrId)
        {
            var typedArray = context.ObtainStyledAttributes(source, groupId);

            try
            {
                return typedArray.GetDimensionPixelSize(attrId, 0);
            }
            finally
            {
                typedArray.Recycle();
            }
        }
    }
}