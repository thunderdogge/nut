using System;
using Nut.Core.Extensions;

namespace Nut.Core.Bindings
{
    public class NutTargetDirectBinding : NutTargetBinding
    {
        private readonly object target;
        private readonly string property;

        public NutTargetDirectBinding(object target, string property)
        {
            this.target = target;
            this.property = property;
        }

        public override void SetValue(object value)
        {
            if (target == null)
            {
                return;
            }

            if (string.IsNullOrEmpty(property))
            {
                return;
            }

            var targetProperty = target.GetType().GetNamedProperty(property);
            if (targetProperty == null)
            {
                return;
            }

            try
            {
                targetProperty.SetValue(target, value);
            }
            catch (Exception exception)
            {
                Nuts.Error($"Property `{property}` binding error: {exception}");
                throw;
            }
        }
    }
}