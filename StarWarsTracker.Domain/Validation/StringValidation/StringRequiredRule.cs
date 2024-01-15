namespace StarWarsTracker.Domain.Validation.StringValidation
{
    /// <summary>
    /// This rule defines that the objectToValidate must not be null, empty, or whitespace
    /// </summary>
    public class StringRequiredRule : ValidationRule<string>
    {
        public StringRequiredRule(string objectToValidate, string nameOfObjectToValidate) : base(objectToValidate, nameOfObjectToValidate) { }

        public override bool IsPassingRule(out string validationFailureMessage)
        {
            if (string.IsNullOrWhiteSpace(ObjectToValidate))
            {
                validationFailureMessage = ValidationFailureMessage.RequiredField(NameOfObjectToValidate);
                return false;
            }

            validationFailureMessage = string.Empty;
            return true;
        }
    }
}
