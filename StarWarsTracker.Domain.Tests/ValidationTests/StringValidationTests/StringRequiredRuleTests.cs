using StarWarsTracker.Domain.Validation;
using StarWarsTracker.Domain.Validation.StringValidation;

namespace StarWarsTracker.Domain.Tests.ValidationTests.StringValidationTests
{
    public class StringRequiredRuleTests
    {
        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("    ")]
        [InlineData(null)]
        public void StringRequiredRule_Given_NullEmptyOrWhiteSpace_IsPassingRule_ShouldReturn_False(string input)
        {
            var rule = new StringRequiredRule(input, nameof(input));

            var result = rule.IsPassingRule(out _);

            Assert.False(result);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("    ")]
        [InlineData(null)]
        public void StringRequiredRule_Given_NullEmptyOrWhiteSpace_IsPassingRule_ShouldPutOut_ValidationFailureMessage_RequiredField(string input)
        {
            var expectedFailureMessage = ValidationFailureMessage.RequiredField(nameof(input));

            var rule = new StringRequiredRule(input, nameof(input));

            rule.IsPassingRule(out var validationFailureMessage);

            Assert.Equal(expectedFailureMessage, validationFailureMessage);
        }

        [Theory]
        [InlineData("A")]
        [InlineData("ABC")]
        [InlineData("defghijklmnop")]
        [InlineData("123")]
        [InlineData("!@#")]
        public void StringRequiredRule_Given_StringIsNotNullEmptyOrWhiteSpace_IsPassingRule_ShouldReturn_True(string input)
        {
            var rule = new StringRequiredRule(input, nameof(input));

            Assert.True(rule.IsPassingRule(out _));
        }
    }
}
