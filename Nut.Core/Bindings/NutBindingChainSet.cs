using System.Collections.Generic;
using Nut.Ioc;

namespace Nut.Core.Bindings
{
    [NutIocIgnore]
    public class NutBindingChainSet<TSource>
    {
        private readonly INutBindingStore bindingStore;
        private readonly INutBindingCreator bindingCreator;
        private readonly List<INutBindingChain> chains = new List<INutBindingChain>();

        public NutBindingChainSet(INutBindingStore bindingStore, INutBindingCreator bindingCreator)
        {
            this.bindingStore = bindingStore;
            this.bindingCreator = bindingCreator;
        }

        public NutBindingChain<TTarget, TSource> Bind<TTarget>(TTarget target) where TTarget : class
        {
            var chain = new NutBindingChain<TTarget, TSource>(bindingStore, target, bindingCreator);
            chains.Add(chain);

            return chain;
        }

        public NutBindingChain<TTarget, TSource> BindTap<TTarget>(TTarget target) where TTarget : class
        {
            return Bind(target).For("Tap");
        }

        public NutBindingChain<TTarget, TSource> BindImage<TTarget>(TTarget target) where TTarget : class
        {
            return Bind(target).For("Image");
        }

        public NutBindingChain<TTarget, TSource> BindChecked<TTarget>(TTarget target) where TTarget : class
        {
            return Bind(target).For("Checked");
        }

        public NutBindingChain<TTarget, TSource> BindText<TTarget>(TTarget target) where TTarget : class
        {
            return Bind(target).For("Text");
        }

        public NutBindingChain<TTarget, TSource> BindBlur<TTarget>(TTarget target) where TTarget : class
        {
            return Bind(target).For("Blur");
        }

        public NutBindingChain<TTarget, TSource> BindEditDone<TTarget>(TTarget target) where TTarget : class
        {
            return Bind(target).For("EditDone");
        }

        public NutBindingChain<TTarget, TSource> BindTextColor<TTarget>(TTarget target) where TTarget : class
        {
            return Bind(target).For("TextColor");
        }

        public NutBindingChain<TTarget, TSource> BindBackgroundColor<TTarget>(TTarget target) where TTarget : class
        {
            return Bind(target).For("BackgroundColor");
        }

        public NutBindingChain<TTarget, TSource> BindValidation<TTarget>(TTarget target) where TTarget : class
        {
            return Bind(target).For("Validation");
        }

        public NutBindingChain<TTarget, TSource> BindHidden<TTarget>(TTarget target) where TTarget : class
        {
            return Bind(target).For("Hidden");
        }

        public NutBindingChain<TTarget, TSource> BindUnhidden<TTarget>(TTarget target) where TTarget : class
        {
            return BindHidden(target).WithConversion("InvertedBool");
        }

        public NutBindingChain<TTarget, TSource> BindVisibility<TTarget>(TTarget target) where TTarget : class
        {
            return Bind(target).For("Visibility");
        }

        public NutBindingChain<TTarget, TSource> BindInvisibility<TTarget>(TTarget target) where TTarget : class
        {
            return BindVisibility(target).WithConversion("InvertedBool");
        }

        public NutBindingChain<TTarget, TSource> BindSource<TTarget>(TTarget target) where TTarget : class
        {
            return Bind(target).For("Source");
        }

        public NutBindingChain<TTarget, TSource> BindSourceSelect<TTarget>(TTarget target) where TTarget : class
        {
            return Bind(target).For("SourceSelect");
        }

        public void Apply()
        {
            foreach (var chain in chains)
            {
                chain.RegisterBinding();
            }

            bindingStore.BindingContext.ApplyBindings();
            chains.Clear();
        }
    }
}