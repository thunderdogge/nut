using System;
using Nut.Core.Bindings.Commands;
using Nut.Ioc;

namespace Nut.Core.Bindings
{
    [NutIocIgnore]
    public abstract class NutTargetCommandBinding : NutTargetBinding
    {
        public override void SetValue(object value)
        {
            if (value == null)
            {
                return;
            }

            var command = value as INutCommand;
            if (command == null)
            {
                throw new ArgumentException("Target binding property must be convertible to `{0}`", nameof(INutCommand));
            }

            SetCommandValue(command);
        }

        protected abstract void SetCommandValue(INutCommand command);
    }
}