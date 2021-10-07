using System.Collections.Generic;
using Nut.Ioc;

namespace Nut.Core.Bindings
{
    [NutIocIgnore]
    public class NutBindingContext : INutBindingContext
    {
        private readonly List<INutBinding> bindings = new List<INutBinding>();

        public object DataSource { get; set; }

        public void ApplyBindings()
        {
            foreach (var binding in bindings)
            {
                binding.Apply(DataSource);
            }
        }

        public void RegisterBinding(INutBinding binding)
        {
            bindings.Add(binding);
        }

        public void UnregisterBindings()
        {
            foreach (var binding in bindings)
            {
                binding.Dispose();
            }

            bindings.Clear();
        }
    }
}