using StarWarsTracker.Domain.Validation;
using StarWarsTracker.Domain.Validation.GuidValidation;

namespace StarWarsTracker.Domain.Tests.ValidationTests.GuidValidationTests
{
    public class GuidRequiredRuleTests
    {
        [Fact]
        public void GuidRequiredRule_Given_EmptyGuid_IsPassingRule_ShouldReturn_False()
        {
            var guid = Guid.Empty;

            var rule = new GuidRequiredRule(guid, nameof(guid));

            var result = rule.IsPassingRule(out _);

            Assert.False(result);
        }

        [Fact]
        public void GuidRequiredRule_Given_NullGuid_IsPassingRule_ShouldReturn_False()
        {
            Guid? guid = null;

            var rule = new GuidRequiredRule(guid, nameof(guid));

            var result = rule.IsPassingRule(out _);

            Assert.False(result);
        }

        [Fact]
        public void GuidRequiredRule_Given_EmptyGuid_IsPassingRule_ShouldPutOut_ValidationFailureMessage_RequiredField()
        {
            var guid = Guid.Empty;

            var expectedFailureMessage = ValidationFailureMessage.RequiredField(nameof(guid));

            var rule = new GuidRequiredRule(guid, nameof(guid));

            rule.IsPassingRule(out var validationFailureMessage);

            Assert.Equal(expectedFailureMessage, validationFailureMessage);
        }

        [Fact]
        public void GuidRequiredRule_Given_NullGuid_IsPassingRule_ShouldPutOut_ValidationFailureMessage_RequiredField()
        {
            Guid? guid = null;

            var expectedFailureMessage = ValidationFailureMessage.RequiredField(nameof(guid));

            var rule = new GuidRequiredRule(guid, nameof(guid));

            rule.IsPassingRule(out var validationFailureMessage);

            Assert.Equal(expectedFailureMessage, validationFailureMessage);
        }

        [Fact]
        public void GuidRequiredRule_Given_ValidGuid_IsPassingRule_ShouldReturn_True()
        {
            var guid = Guid.NewGuid();

            var rule = new GuidRequiredRule(guid, nameof(guid));

            Assert.True(rule.IsPassingRule(out _));
        }

        [Fact]
        public void GuidRequiredRule_Given_ValidGuid_IsPassingRule_ShouldPutOut_EmptyString()
        {
            var guid = Guid.NewGuid();

            var rule = new GuidRequiredRule(guid, nameof(guid));

            rule.IsPassingRule(out var result);

            Assert.Empty(result);
        }
    }
}
