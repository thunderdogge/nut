using System;
using Nut.Core.Bindings;

namespace Nut.Core.Views
{
    public interface INutView : INutBindingStore
    {
        Guid Identifier { get; set; }
    }
}