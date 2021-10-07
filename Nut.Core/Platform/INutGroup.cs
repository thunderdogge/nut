using System.Collections;

namespace Nut.Core.Platform
{
    public interface INutGroup
    {
        object Key { get; set; }

        IEnumerable Items { get; set; }
    }
}