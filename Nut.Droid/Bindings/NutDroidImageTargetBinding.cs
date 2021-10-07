using System;
using Android.Graphics;
using Android.Widget;
using Nut.Core.Bindings;
using Nut.Droid.Extensions;

namespace Nut.Droid.Bindings
{
    public class NutDroidImageTargetBinding : NutTargetBinding
    {
        private readonly ImageView target;
        private int? imageHash;

        public NutDroidImageTargetBinding(ImageView target)
        {
            this.target = target;
        }

        public override void SetValue(object value)
        {
            var base64 = value as string;
            if (string.IsNullOrEmpty(base64))
            {
                SetValueInternalEmpty();
                return;
            }

            var base64Hash = base64.GetHashCode();
            if (!imageHash.HasValue || imageHash.Value != base64Hash)
            {
                imageHash = base64Hash;
                SetValueInternal(base64);
            }
        }

        protected virtual void SetValueInternal(string base64)
        {
            var bytesArray = Convert.FromBase64String(base64);
            using (var bitmap = BitmapFactory.DecodeByteArray(bytesArray, 0, bytesArray.Length))
            {
                target.RecycleBitmap();
                target.SetImageBitmap(bitmap);
            }
        }

        protected virtual void SetValueInternalEmpty()
        {
            if (imageHash.HasValue)
            {
                target.RecycleBitmap();
                imageHash = null;
            }

            target.SetImageDrawable(null);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (target != null)
                {
                    target.RecycleBitmap();
                    imageHash = null;
                }
            }

            base.Dispose(disposing);
        }
    }
}