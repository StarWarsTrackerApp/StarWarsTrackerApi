using StarWarsTracker.Domain.Validation;
using StarWarsTracker.Domain.Validation.StringValidation;

namespace StarWarsTracker.Domain.Tests.ValidationTests.StringValidationTests
{
    public class StringLengthLimitRuleTests
    {
        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("    ")]
        [InlineData(null)]
        public void StringLengthLimitRule_Given_StringIsRequired_AndInput_IsNullEmptyOrWhiteSpace_IsPassingRule_ShouldReturn_False(string input)
        {
            var rule = new StringLengthLimitRule(input, nameof(input), maxLength: int.MaxValue, isRequiredString: true);

            var result = rule.IsPassingRule(out _);

            Assert.False(result);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("    ")]
        [InlineData(null)]
        public void StringLengthLimitRule_Given_StringIsRequired_AndInput_IsNullEmptyOrWhiteSpace_IsPassingRule_ShouldPutOut_ValidationFailureMessage_RequiredField(string input)
        {
            var expectedFailureMessage = ValidationFailureMessage.RequiredField(nameof(input));

            var rule = new StringLengthLimitRule(input, nameof(input), maxLength: int.MaxValue, isRequiredString: true);

            rule.IsPassingRule(out var validationFailureMessage);

            Assert.Equal(expectedFailureMessage, validationFailureMessage);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("    ")]
        [InlineData(null)]
        public void StringLengthLimitRule_Given_StringIsNotRequired_AndInput_IsNullEmptyOrWhiteSpace_IsPassingRule_ShouldReturn_True(string input)
        {
            var rule = new StringLengthLimitRule(input, nameof(input), maxLength: int.MaxValue, isRequiredString: false);

            var result = rule.IsPassingRule(out _);

            Assert.True(result);
        }

        [Theory]
        [InlineData("12", 1)]
        [InlineData("123", 1)]
        [InlineData("12345", 3)]
        [InlineData("123456789", 8)]
        [InlineData("123456 10 123456 20 123456789", 25)]
        public void StringLengthLimitRule_Given_StringIsExceedingLimit_IsPassingRule_ShouldReturn_False(string input, int maxLength)
        {
            var rule = new StringLengthLimitRule(input, nameof(input), maxLength);

            Assert.False(rule.IsPassingRule(out _));
        }

        [Theory]
        [InlineData("12", 1)]
        [InlineData("123", 1)]
        [InlineData("12345", 3)]
        [InlineData("123456789", 8)]
        [InlineData("123456 10 123456 20 123456789", 25)]
        public void StringLengthLimitRule_Given_StringIsExceedingLimit_IsPassingRule_ShouldPutOut_ValidationFailureMessage_RequiredField(string input, int maxLength)
        {
            var expectedFailureMessage = ValidationFailureMessage.StringExceedingMaxLength(input, nameof(input), maxLength);

            var rule = new StringLengthLimitRule(input, nameof(input), maxLength);

            rule.IsPassingRule(out var validationFailureMessage);

            Assert.Equal(expectedFailureMessage, validationFailureMessage);
        }

        [Theory]
        [InlineData("123", 5)]
        [InlineData("12345", 8)]
        [InlineData("1234567 10", 15)]
        [InlineData("123456", 15)]
        [InlineData("123456 10 123456 20 123456789", 30)]
        public void StringLengthLimitRule_Given_StringIsNotExceedingLimit_IsPassingRule_ShouldReturn_True(string input, int maxLength)
        {
            var rule = new StringLengthLimitRule(input, nameof(input), maxLength);

            Assert.True(rule.IsPassingRule(out _));
        }
    }
}
