using Nut.Core.Models.Validation;
using Nut.Core.Tests.Base;
using Nut.Core.Tests.Mocks;
using NUnit.Framework;

namespace Nut.Core.Tests.TestModels
{
    public class TestValidation : TestBase
    {
        [TestCase(null, true, "")]
        [TestCase("", true, "")]
        [TestCase(" ", false, "Invalid value")]
        [TestCase("a", false, "Invalid value")]
        [TestCase("a@", false, "Invalid value")]
        [TestCase("a@b", false, "Invalid value")]
        [TestCase("a@b.", false, "Invalid value")]
        [TestCase("a@b.com", true, "")]
        [TestCase("a.b@b.com", true, "")]
        [TestCase("a.b_@b.com", true, "")]
        public void TestEmailValidation(string propertyValue, bool expectedIsValid, string expectedError)
        {
            var validator = new NutValidator();
            var viewModel = new MockViewModel3 { Property1 = propertyValue };
            validator.For(viewModel, x => x.Property1, x => x.Property1Error).Email("Invalid value");

            var result = validator.Validate();
            Assert.AreEqual(expectedIsValid, result.IsValid);
            Assert.AreEqual(expectedError, viewModel.Property1Error);
        }

        [TestCase(null, true, "")]
        [TestCase("", true, "")]
        [TestCase(" ", false, "Invalid value")]
        [TestCase("a", false, "Invalid value")]
        [TestCase("+1234567", true, "")]
        public void TestPhoneValidation(string propertyValue, bool expectedIsValid, string expectedError)
        {
            var validator = new NutValidator();
            var viewModel = new MockViewModel3 { Property1 = propertyValue };
            validator.For(viewModel, x => x.Property1, x => x.Property1Error).Phone("Invalid value");

            var result = validator.Validate();
            Assert.AreEqual(expectedIsValid, result.IsValid);
            Assert.AreEqual(expectedError, viewModel.Property1Error);
        }

        [TestCase(null, true, 1, 2, "")]
        [TestCase("", true, 0, 2, "")]
        [TestCase("", false, 1, 2, "Invalid value")]
        [TestCase("abc", false, 1, 2, "Invalid value")]
        [TestCase("a", true, 1, 2, "")]
        [TestCase("ab", true, 1, 2, "")]
        public void TestLengthValidation(string propertyValue, bool expectedIsValid, int minLength, int maxLength, string expectedError)
        {
            var validator = new NutValidator();
            var viewModel = new MockViewModel3 { Property1 = propertyValue };
            validator.For(viewModel, x => x.Property1, x => x.Property1Error).Length(minLength, maxLength, "Invalid value");

            var result = validator.Validate();
            Assert.AreEqual(expectedIsValid, result.IsValid);
            Assert.AreEqual(expectedError, viewModel.Property1Error);
        }

        [TestCase(null, true, 2, "")]
        [TestCase("", true, 2, "")]
        [TestCase("abc", false, 2, "Invalid value")]
        [TestCase("a", true, 2, "")]
        [TestCase("ab", true, 2, "")]
        public void TestMaxLengthValidation(string propertyValue, bool expectedIsValid, int maxLength, string expectedError)
        {
            var validator = new NutValidator();
            var viewModel = new MockViewModel3 { Property1 = propertyValue };
            validator.For(viewModel, x => x.Property1, x => x.Property1Error).MaxLength(maxLength, "Invalid value");

            var result = validator.Validate();
            Assert.AreEqual(expectedIsValid, result.IsValid);
            Assert.AreEqual(expectedError, viewModel.Property1Error);
        }

        [TestCase(null, false, "Value is required")]
        [TestCase("", false, "Value is required")]
        [TestCase(" ", false, "Value is required")]
        [TestCase("somename", true, "")]
        public void TestRequiredValidation(string propertyValue, bool expectedIsValid, string expectedError)
        {
            var validator = new NutValidator();
            var viewModel = new MockViewModel3 { Property1 = propertyValue };
            validator.For(viewModel, x => x.Property1, x => x.Property1Error).Required("Value is required");

            var result = validator.Validate();
            Assert.AreEqual(expectedIsValid, result.IsValid);
            Assert.AreEqual(expectedError, viewModel.Property1Error);
        }

        [TestCase(null, null, false, "Invalid value")]
        [TestCase("", "", false, "Invalid value")]
        [TestCase("Ab", "Ab", false, "Invalid value")]
        [TestCase("ab", "ab", true, "")]
        public void TestPredicateValidation(string property1Value, string property2Value, bool expectedIsValid, string expectedError)
        {
            var validator = new NutValidator();
            var viewModel = new MockViewModel3 { Property1 = property1Value, Property2 = property2Value };
            validator.For(viewModel, x => x.Property1, x => x.Property1Error).ValidIf(x => !string.IsNullOrEmpty(x) && x == x.ToLower(), "Invalid value");
            validator.For(viewModel, x => x.Property2, x => x.Property2Error).InvalidIf(x => string.IsNullOrEmpty(x) || x != x.ToLower(), "Invalid value");

            var result = validator.Validate();
            Assert.AreEqual(expectedIsValid, result.IsValid);
            Assert.AreEqual(expectedError, viewModel.Property1Error);
            Assert.AreEqual(expectedError, viewModel.Property2Error);
        }
    }
}