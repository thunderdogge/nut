using System;

namespace Nut.Core.Models.Validation
{
    public class NutPredicateValidation<TViewModel> : NutValidationBase<TViewModel>
    {
        private readonly Func<string, bool> predicate;

        public NutPredicateValidation(TViewModel source,
                                      Func<TViewModel, string> valueProperty,
                                      string errorMessage,
                                      Func<string, bool> predicate) : base(source, valueProperty, errorMessage)
        {
            this.predicate = predicate;
        }

        protected override bool IsValidInternal(string value)
        {
            return predicate(value);
        }
    }
}