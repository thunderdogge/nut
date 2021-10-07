using System;
using System.Collections.Generic;
using Nut.Core.Models;

namespace Nut.Core.Views
{
    public class NutViewMapper : INutViewMapper
    {
        private readonly Dictionary<Type, Type> bindingMap = new Dictionary<Type, Type>();

        public void Map(Type viewModelType, Type viewType)
        {
            bindingMap[viewModelType] = viewType;
        }

        public void Map<TViewModel, TView>() where TViewModel : INutViewModel
        {
            Map(typeof(TViewModel), typeof(TView));
        }

        public Type GetViewType(Type viewModelType)
        {
            Type type;
            if (bindingMap.TryGetValue(viewModelType, out type))
            {
                return type;
            }

            throw new KeyNotFoundException($"Could not find view for {viewModelType}. You should specify view mapping in setup.");
        }
    }
}