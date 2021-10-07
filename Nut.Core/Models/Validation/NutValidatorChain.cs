using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Nut.Core.Extensions;

namespace Nut.Core.Models.Validation
{
    public class NutValidatorChain<TViewModel> : INutValidatorChain
    {
        private readonly List<INutValidatable> validations = new List<INutValidatable>();

        private readonly TViewModel source;
        private readonly Func<TViewModel, string> valueProperty;
        private readonly Expression<Func<TViewModel, string>> errorProperty;

        public NutValidatorChain(TViewModel source, Func<TViewModel, string> valueProperty, Expression<Func<TViewModel, string>> errorProperty)
        {
            this.source = source;
            this.valueProperty = valueProperty;
            this.errorProperty = errorProperty;
        }

        public INutValidatorChain Email(string errorMessage)
        {
            return ValidIf(x => string.IsNullOrEmpty(x) || x.IsEmailFormat(), errorMessage);
        }

        public INutValidatorChain Phone(string errorMessage)
        {
            return ValidIf(x => string.IsNullOrEmpty(x) || x.IsPhoneFormat(), errorMessage);
        }

        public INutValidatorChain Length(int minLength, int maxLength, string errorMessage)
        {
            var validation = new NutLengthValidation<TViewModel>(source, valueProperty, errorMessage, minLength, maxLength);
            return Pipe(validation);
        }

        public INutValidatorChain MaxLength(int maxLength, string errorMessage)
        {
            return Length(0, maxLength, errorMessage);
        }

        public INutValidatorChain Required(string errorMessage)
        {
            return ValidIf(x => !string.IsNullOrWhiteSpace(x), errorMessage);
        }

        public INutValidatorChain ValidIf(Func<string, bool> predicate, string errorMessage)
        {
            var validation = new NutPredicateValidation<TViewModel>(source, valueProperty, errorMessage, predicate);
            return Pipe(validation);
        }

        public INutValidatorChain InvalidIf(Func<string, bool> predicate, string errorMessage)
        {
            return ValidIf(x => !predicate(x), errorMessage);
        }

        public NutValidationResult Validate()
        {
            var errorPropertyInfo = NutExpressionExtensions.GetPropertyInfo(errorProperty);
            foreach (var validation in validations)
            {
                var result = validation.Validate();
                if (result != null && result.IsInvalid)
                {
                    errorPropertyInfo.SetValue(source, result.Message);
                    return result;
                }
            }

            errorPropertyInfo.SetValue(source, string.Empty);
            return null;
        }

        private INutValidatorChain Pipe(INutValidatable validatable)
        {
            validations.Add(validatable);
            return this;
        }
    }
}