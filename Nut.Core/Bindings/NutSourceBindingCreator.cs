namespace Nut.Core.Bindings
{
    public class NutSourceBindingCreator : INutSourceBindingCreator
    {
        public INutSourceBinding Create(INutBindingDescription bindingDescription)
        {
            return new NutSourceBinding
            {
                Mode = bindingDescription.Mode,
                PropertyName = CreatePropertyName(bindingDescription.SourceProperty),
                PropertyFullName = bindingDescription.SourceProperty
            };
        }

        private static string CreatePropertyName(string propertyFullName)
        {
            var propertyNameParts = propertyFullName.Split('.');
            return propertyNameParts[propertyNameParts.Length - 1];
        }
    }
}