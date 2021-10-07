using System;

namespace Nut.Core.Bindings
{
    public interface INutBinding : IDisposable
    {
        void Apply(object source);
    }
}