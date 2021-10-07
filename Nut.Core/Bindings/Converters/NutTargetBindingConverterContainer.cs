using System;
using System.Collections.Generic;
using Nut.Core.Platform;

namespace Nut.Core.Bindings.Converters
{
    public class NutTargetBindingConverterContainer : NutSingleton<INutTargetBindingConverterContainer>, INutTargetBindingConverterContainer
    {
        private readonly Dictionary<string, INutTargetBindingConverter> bindingConverters = new Dictionary<string, INutTargetBindingConverter>();

        public INutTargetBindingConverter GetBindingConverter(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return null;
            }

            INutTargetBindingConverter bindingConverter;
            if (bindingConverters.TryGetValue(name, out bindingConverter))
            {
                return bindingConverter;
            }

            throw new ArgumentException($"Binding converter `{name}` is not registered");
        }

        public void RegisterBindingConverter(string name, INutTargetBindingConverter bindingConverter)
        {
            if (bindingConverters.ContainsKey(name))
            {
                return;
            }

            bindingConverters.Add(name, bindingConverter);
        }
    }
}