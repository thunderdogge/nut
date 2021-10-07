using System;
using Foundation;
using Nut.Core.Bindings;
using UIKit;

namespace Nut.iOS.Bindings
{
    public class NutIosImageTargetBinding : NutTargetBinding
    {
        private readonly UIImageView target;
        private int? imageHash;

        public NutIosImageTargetBinding(UIImageView target)
        {
            this.target = target;
        }

        public override void SetValue(object value)
        {
            var base64 = value as string;
            if (string.IsNullOrEmpty(base64))
            {
                imageHash = null;
                target.Image = null;
                return;
            }

            var base64Hash = base64.GetHashCode();
            if (imageHash.HasValue && imageHash.Value == base64Hash)
            {
                return;
            }

            var imageBytes = Convert.FromBase64String(base64);
            var imageData = NSData.FromArray(imageBytes);
            target.Image = UIImage.LoadFromData(imageData);
            imageHash = base64Hash;
        }
    }
}