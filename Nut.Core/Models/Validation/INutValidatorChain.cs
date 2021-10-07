using System;

namespace Nut.Core.Models.Validation
{
    public interface INutValidatorChain : INutValidatable
    {
        INutValidatorChain Email(string errorMessage);
        INutValidatorChain Phone(string errorMessage);
        INutValidatorChain Length(int minLength, int maxLength, string errorMessage);
        INutValidatorChain MaxLength(int maxLength, string errorMessage);
        INutValidatorChain Required(string errorMessage);
        INutValidatorChain ValidIf(Func<string, bool> predicate, string errorMessage);
        INutValidatorChain InvalidIf(Func<string, bool> predicate, string errorMessage);
    }
}