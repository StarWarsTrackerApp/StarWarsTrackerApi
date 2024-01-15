using StarWarsTracker.Domain.Enums;
using StarWarsTracker.Domain.Validation;
using StarWarsTracker.Domain.Validation.EnumValidation;

namespace StarWarsTracker.Domain.Tests.ValidationTests.EnumValidationTests
{
    public class RequiredCanonTypeRuleTests
    {
        [Theory]
        [InlineData(CanonType.StrictlyCanon)]
        [InlineData(CanonType.StrictlyLegends)]
        [InlineData(CanonType.CanonAndLegends)]
        public void RequiredCanonType_Given_InputIsValidCanonType_IsPassingRule_ShouldReturn_True(CanonType canonType)
        {
            var rule = new RequiredCanonTypeRule(canonType, nameof(canonType));

            Assert.True(rule.IsPassingRule(out _));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(int.MinValue)]
        [InlineData(int.MaxValue)]
        public void RequiredCanonType_Given_InputIsNotValidCanonType_IsPassingRule_ShouldReturn_False(int canonType)
        {
            var rule = new RequiredCanonTypeRule((CanonType)canonType, nameof(canonType));

            Assert.False(rule.IsPassingRule(out _));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(int.MinValue)]
        [InlineData(int.MaxValue)]
        public void RequiredCanonType_Given_InputIsNotValidCanonType_IsPassingRule_ShouldPutOut_ValidationFailureMessage_InvalidValue(int canonType)
        {
            var expectedFailureMessage = ValidationFailureMessage.InvalidValue(canonType, nameof(canonType));

            var rule = new RequiredCanonTypeRule((CanonType)canonType, nameof(canonType));

            rule.IsPassingRule(out var validationFailureMessage);

            Assert.Equal(expectedFailureMessage, validationFailureMessage);
        }
    }
}
