using System;
using System.Linq.Expressions;
using Nut.Core.Bindings.Exceptions;
using Nut.Core.Extensions;
using Nut.Ioc;

namespace Nut.Core.Bindings
{
    [NutIocIgnore]
    public class NutBindingChain<TTarget, TSource> : INutBindingChain where TTarget : class
    {
        private readonly TTarget bindingTarget;
        private readonly INutBindingStore bindingStore;
        private readonly INutBindingCreator bindingCreator;
        private INutBindingDescription bindingDescription;

        public NutBindingChain(INutBindingStore bindingStore, TTarget bindingTarget, INutBindingCreator bindingCreator)
        {
            this.bindingStore = bindingStore;
            this.bindingTarget = bindingTarget;
            this.bindingCreator = bindingCreator;
        }

        public INutBindingDescription BindingDescription
        {
            get { return bindingDescription ?? (bindingDescription = new NutBindingDescription()); }
            set { bindingDescription = value; }
        }

        public NutBindingChain<TTarget, TSource> For(string targetProperty)
        {
            BindingDescription.TargetProperty = targetProperty;
            return this;
        }

        public NutBindingChain<TTarget, TSource> For(Expression<Func<TTarget, object>> targetProperty)
        {
            var propertyPath = NutExpressionExtensions.GetPropertyPath(targetProperty);
            return For(propertyPath);
        }

        public NutBindingChain<TTarget, TSource> OneWay()
        {
            BindingDescription.Mode = NutBindingMode.OneWay;
            return this;
        }

        public NutBindingChain<TTarget, TSource> TwoWay()
        {
            BindingDescription.Mode = NutBindingMode.TwoWay;
            return this;
        }

        public NutBindingChain<TTarget, TSource> WithConversion(string converterName)
        {
            BindingDescription.ConverterName = converterName;
            return this;
        }

        public NutBindingChain<TTarget, TSource> WithConversion(Func<object, object> converterMethod)
        {
            BindingDescription.ConverterMethod = converterMethod;
            return this;
        }

        public NutBindingChain<TTarget, TSource> To(string sourceProperty)
        {
            BindingDescription.SourceProperty = sourceProperty;
            return this;
        }

        public NutBindingChain<TTarget, TSource> To(Expression<Func<TSource, object>> sourceProperty)
        {
            var propertyPath = NutExpressionExtensions.GetPropertyPath(sourceProperty);
            return To(propertyPath);
        }

        public void RegisterBinding()
        {
            if (bindingTarget == null)
            {
                throw new NutBindingException("Binding target is missing");
            }

            var description = BindingDescription;
            if (string.IsNullOrEmpty(description.TargetProperty))
            {
                throw new NutBindingException("Binding target property is empty");
            }

            if (string.IsNullOrEmpty(description.SourceProperty))
            {
                throw new NutBindingException("Binding source property is empty");
            }

            var binding = bindingCreator.Create(bindingTarget, BindingDescription);
            bindingStore.BindingContext.RegisterBinding(binding);
        }
    }
}