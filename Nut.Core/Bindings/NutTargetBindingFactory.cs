using System;

namespace Nut.Core.Bindings
{
    public class NutTargetBindingFactory<TView> : INutTargetBindingFactory where TView : class 
    {
        private readonly Func<TView, INutTargetBinding> creationFunc;

        public NutTargetBindingFactory(Func<TView, INutTargetBinding> creationFunc)
        {
            this.creationFunc = creationFunc;
        }

        public INutTargetBinding CreateBinding(object target)
        {
            if (target == null)
            {
                throw new ArgumentException("Binding target is missing");
            }

            var control = target as TView;
            if (control == null)
            {
                var expectedType = typeof(TView).FullName;
                var actualType = target.GetType().FullName;
                throw new ArgumentException($"Expecting binding target of type `{expectedType}`, but was `{actualType}`");
            }

            return creationFunc(control);
        }
    }
}