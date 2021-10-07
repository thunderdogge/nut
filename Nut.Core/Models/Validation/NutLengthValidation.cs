using System;

namespace Nut.Core.Models.Validation
{
    public class NutLengthValidation<TViewModel> : NutValidationBase<TViewModel>
    {
        private readonly int minLength;
        private readonly int maxLength;

        public NutLengthValidation(TViewModel source,
                                   Func<TViewModel, string> valueProperty,
                                   string errorMessage,
                                   int minLength,
                                   int maxLength) : base(source, valueProperty, errorMessage)
        {
            this.minLength = minLength;
            this.maxLength = maxLength;
        }

        protected override bool IsValidInternal(string value)
        {
            if (value == null || value.Length == 0 && minLength == 0)
            {
                return true;
            }

            return minLength <= value.Length && value.Length <= maxLength;
        }
    }
}