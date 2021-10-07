using System;
using Nut.Core.Models;

namespace Nut.Core.Views
{
    public interface INutViewMapper
    {
        void Map(Type viewModelType, Type viewType);
        void Map<TViewModel, TView>() where TViewModel : INutViewModel;
        Type GetViewType(Type viewModelType);
    }
}