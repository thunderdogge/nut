using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Nut.Core.Models.Validation
{
    public class NutValidator : INutValidator
    {
        private readonly List<INutValidatorChain> validatorChains = new List<INutValidatorChain>();

        public INutValidatorChain For<TViewModel>(TViewModel source, Func<TViewModel, string> valueProperty, Expression<Func<TViewModel, string>> errorProperty)
        {
            var chain = new NutValidatorChain<TViewModel>(source, valueProperty, errorProperty);
            validatorChains.Add(chain);

            return chain;
        }

        public NutValidationResult Validate()
        {
            var isValid = true;
            foreach (var chain in validatorChains)
            {
                var chainResult = chain.Validate();
                if (chainResult != null && chainResult.IsInvalid)
                {
                    isValid = false;
                }
            }

            return new NutValidationResult(isValid);
        }
    }
}