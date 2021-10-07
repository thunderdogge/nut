using Android.Views;
using Nut.Core.Bindings;
using Nut.Core.Platform;
using Nut.Droid.Extensions;

namespace Nut.Droid.Bindings
{
    public class NutDroidBackgroundColorTargetBinding : NutTargetColorBinding
    {
        private readonly View target;

        public NutDroidBackgroundColorTargetBinding(View target)
        {
            this.target = target;
        }

        protected override void SetColorValue(NutColor color)
        {
            target.SetBackgroundColor(color.ToNative());
        }
    }
}