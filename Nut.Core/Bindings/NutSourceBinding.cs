using System;
using System.Reflection;
using Nut.Core.Extensions;
using Nut.Ioc;

namespace Nut.Core.Bindings
{
    [NutIocIgnore]
    public class NutSourceBinding : INutSourceBinding
    {
        private object dataSource;
        private object bindingSource;
        private object bindingValue;
        private PropertyInfo bindingProperty;

        public object DataSource
        {
            get { return dataSource; }
            set
            {
                if (dataSource == value)
                {
                    return;
                }

                var bindableSource = GetBindingSource(value);
                var bindableProperty = GetBindingProperty(bindableSource);

                dataSource = value;
                bindingSource = bindableSource;
                bindingProperty = bindableProperty;
            }
        }

        public object BindingSource => bindingSource;

        public NutBindingMode Mode { get; set; }

        public string PropertyName { get; set; }

        public string PropertyFullName { get; set; }

        public object GetValue()
        {
            var currentValue = GetBindingValue(bindingSource, bindingProperty);
            bindingValue = currentValue;

            return bindingValue;
        }

        public void SetValue(object newValue)
        {
            if (Equals(bindingValue, newValue))
            {
                return;
            }

            bindingValue = newValue;
            bindingProperty.SetValue(bindingSource, newValue);
        }

        private object GetBindingSource(object value)
        {
            var propertyPathPart = PropertyFullName.Split('.');
            if (propertyPathPart.Length == 1)
            {
                return value;
            }

            var currentValue = value;
            for (var i = 0; i < propertyPathPart.Length - 1; i++)
            {
                var propertyInfo = currentValue.GetType().GetNamedProperty(propertyPathPart[i]);
                currentValue = propertyInfo.GetValue(currentValue);
            }

            return currentValue;
        }

        private PropertyInfo GetBindingProperty(object bindableSource)
        {
            var propertyPathPart = PropertyFullName.Split('.');
            var propertyPathLastPart = propertyPathPart[propertyPathPart.Length - 1];

            return bindableSource.GetType().GetNamedProperty(propertyPathLastPart);
        }

        private static object GetBindingValue(object bindableSource, PropertyInfo bindableProperty)
        {
            return bindableProperty.GetValue(bindableSource);
        }

        ~NutSourceBinding()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                bindingValue = null;
                bindingSource = null;
                bindingProperty = null;
            }
        }
    }
}