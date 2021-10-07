using System.Collections.Generic;
using Nut.Core.Platform;

namespace Nut.Core.Bindings
{
    public class NutTargetBindingContainer : NutSingleton<INutTargetBindingContainer>, INutTargetBindingContainer
    {
        private readonly Dictionary<string, INutTargetBindingFactory> bindingFactories = new Dictionary<string, INutTargetBindingFactory>();

        public INutTargetBinding GetTargetBinding(object target, string property)
        {
            INutTargetBindingFactory targetBinding;
            if (bindingFactories.TryGetValue(property, out targetBinding))
            {
                return targetBinding.CreateBinding(target);
            }

            return new NutTargetDirectBinding(target, property);
        }

        public void RegisterTargetBindingFactory(string name, INutTargetBindingFactory factory)
        {
            bindingFactories[name] = factory;
        }
    }
}