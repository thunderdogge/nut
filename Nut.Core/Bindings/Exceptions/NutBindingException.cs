using System;

namespace Nut.Core.Bindings.Exceptions
{
    public class NutBindingException : Exception
    {
        public NutBindingException(string message) : base(message)
        {
        }
    }
}