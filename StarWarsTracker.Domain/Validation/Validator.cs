namespace StarWarsTracker.Domain.Validation
{
    public class Validator
    {
        private readonly List<string> _validationFailureReasons = new List<string>();

        public List<string> ReasonsForFailure => _validationFailureReasons;

        public bool IsPassingAllRules => _validationFailureReasons.Count == 0;

        public void ApplyRule<T>(IValidationRule<T> validationRule)
        {
            if (!validationRule.IsPassingRule(out var validationFailureMessage))
            {
                _validationFailureReasons.Add(validationFailureMessage);
            }
        }
    }
}
