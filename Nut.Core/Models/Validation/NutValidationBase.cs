using System;

namespace Nut.Core.Models.Validation
{
    public abstract class NutValidationBase<TViewModel> : INutValidatable
    {
        private readonly TViewModel source;
        private readonly Func<TViewModel, string> valueProperty;
        private readonly string errorMessage;

        protected NutValidationBase(TViewModel source, Func<TViewModel, string> valueProperty, string errorMessage)
        {
            this.source = source;
            this.valueProperty = valueProperty;
            this.errorMessage = errorMessage;
        }

        public NutValidationResult Validate()
        {
            var value = valueProperty.Invoke(source);
            if (IsValidInternal(value))
            {
                return null;
            }

            return new NutValidationResult(false, errorMessage);
        }

        protected abstract bool IsValidInternal(string value);
    }
}