namespace Nut.Core.Models.Validation
{
    public class NutValidationResult
    {
        public NutValidationResult(bool isValid) : this(isValid, null)
        {
        }

        public NutValidationResult(bool isValid, string message)
        {
            IsValid = isValid;
            IsInvalid = !isValid;
            Message = message;
        }

        public bool IsValid { get; }

        public bool IsInvalid { get; }

        public string Message { get; }
    }
}