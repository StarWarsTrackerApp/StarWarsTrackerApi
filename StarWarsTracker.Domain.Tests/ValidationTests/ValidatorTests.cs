using StarWarsTracker.Domain.Validation;
using StarWarsTracker.Domain.Validation.StringValidation;

namespace StarWarsTracker.Domain.Tests.ValidationTests
{
    public class ValidatorTests
    {
        [Fact]
        public void Validator_Given_RuleNotPassing_ShouldReturn_IsPassingAllRules_False()
        {
            var validator = new Validator();

            string invalidString = null!;

            validator.ApplyRule(new StringRequiredRule(invalidString, nameof(invalidString)));

            Assert.False(validator.IsPassingAllRules);
        }

        [Fact]
        public void Validator_Given_RuleNotPassing_ReasonsForFailure_ShouldContain_RuleValidationFailureMessage()
        {
            var validator = new Validator();

            string invalidString = null!;

            var rule = new StringRequiredRule(invalidString, nameof(invalidString));

            // get the expected failure message from the rule
            rule.IsPassingRule(out var expectedFailureMessage);

            validator.ApplyRule(rule);

            Assert.Equal(expectedFailureMessage, validator.ReasonsForFailure.Single());
        }
    }
}
