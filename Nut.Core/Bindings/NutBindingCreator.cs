using Nut.Core.Bindings.Converters;

namespace Nut.Core.Bindings
{
    public class NutBindingCreator : INutBindingCreator
    {
        private readonly INutSourceBindingCreator sourceBindingCreator;
        private readonly INutTargetBindingContainer bindingContainer;
        private readonly INutTargetBindingConverterContainer bindingConverterContainer;

        public NutBindingCreator(INutSourceBindingCreator sourceBindingCreator,
                                 INutTargetBindingContainer bindingContainer,
                                 INutTargetBindingConverterContainer bindingConverterContainer)
        {
            this.bindingContainer = bindingContainer;
            this.sourceBindingCreator = sourceBindingCreator;
            this.bindingConverterContainer = bindingConverterContainer;
        }

        public NutBinding Create(object bindingTarget, INutBindingDescription bindingDescription)
        {
            var sourceBinding = sourceBindingCreator.Create(bindingDescription);
            var targetBinding = bindingContainer.GetTargetBinding(bindingTarget, bindingDescription.TargetProperty);
            var targetBindingConverter = CreateBindingConverter(bindingDescription);

            return new NutBinding(targetBinding, sourceBinding, targetBindingConverter);
        }

        private INutTargetBindingConverter CreateBindingConverter(INutBindingDescription bindingDescription)
        {
            if (bindingDescription.ConverterMethod != null)
            {
                return new NutUnifiedTargetBindingConverter(bindingDescription.ConverterMethod);
            }

            return bindingConverterContainer.GetBindingConverter(bindingDescription.ConverterName);
        }
    }
}