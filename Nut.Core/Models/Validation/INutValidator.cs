using System;
using System.Linq.Expressions;

namespace Nut.Core.Models.Validation
{
    public interface INutValidator : INutValidatable
    {
        INutValidatorChain For<TViewModel>(TViewModel source, Func<TViewModel, string> valueProperty, Expression<Func<TViewModel, string>> errorProperty);
    }
}