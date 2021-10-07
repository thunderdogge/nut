using Android.Widget;
using Nut.Core.Bindings;
using Nut.Core.Platform;
using Nut.Droid.Extensions;

namespace Nut.Droid.Bindings
{
    public class NutDroidTextColorTargetBinding : NutTargetColorBinding
    {
        private readonly TextView target;

        public NutDroidTextColorTargetBinding(TextView target)
        {
            this.target = target;
        }

        protected override void SetColorValue(NutColor color)
        {
            target.SetTextColor(color.ToNative());
        }
    }
}