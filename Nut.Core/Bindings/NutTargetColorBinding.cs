using System;
using Nut.Core.Platform;
using Nut.Ioc;

namespace Nut.Core.Bindings
{
    [NutIocIgnore]
    public abstract class NutTargetColorBinding : NutTargetBinding
    {
        public override void SetValue(object value)
        {
            if (value == null)
            {
                return;
            }

            var color = value as NutColor;
            if (color == null)
            {
                throw new ArgumentException("Target binding property should be inherited from {0}", nameof(NutColor));
            }

            SetColorValue(color);
        }

        protected abstract void SetColorValue(NutColor color);
    }
}