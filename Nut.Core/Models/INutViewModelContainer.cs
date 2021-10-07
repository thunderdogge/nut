using System;

namespace Nut.Core.Models
{
    public interface INutViewModelContainer
    {
        INutViewModel GetOrCreateViewModel(Guid viewId, Type viewModelType, string viewModelParameters);
        void RemoveViewModel(Guid viewId);
    }
}