using System;
using System.Collections;
using Nut.Core.Bindings.Commands;

namespace Nut.Core.Bindings
{
    public interface INutCollectionSource : IDisposable
    {
        IEnumerable Items { get; set; }
        INutCommand ItemSelectCommand { get; set; }
    }
}