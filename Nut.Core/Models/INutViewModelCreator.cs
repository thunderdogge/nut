using System;

namespace Nut.Core.Models
{
    public interface INutViewModelCreator
    {
        INutViewModel CreateViewModel(Type viewModelType);
    }
}