namespace Nut.Core.Bindings.Extensions
{
    public static class NutBindingExtensions
    {
        private static readonly INutBindingCreator bindingCreator;

        static NutBindingExtensions()
        {
            bindingCreator = Nuts.Resolve<INutBindingCreator>();
        }

        public static NutBindingChainSet<TSource> CreateBindingSet<TSource>(this INutBindingStore bindingStore)
        {
            return new NutBindingChainSet<TSource>(bindingStore, bindingCreator);
        }
    }
}