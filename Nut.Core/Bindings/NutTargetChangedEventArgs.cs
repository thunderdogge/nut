using System;

namespace Nut.Core.Bindings
{
    public class NutTargetChangedEventArgs : EventArgs
    {
        public NutTargetChangedEventArgs(object value)
        {
            Value = value;
        }

        public object Value { get; private set; }
    }
}