namespace StarWarsTracker.Domain.Validation
{
    public interface IValidationRule<TypeToValidate>
    {
        public TypeToValidate ObjectToValidate { get; set; }

        public string NameOfObjectToValidate { get; set; }

        public bool IsPassingRule(out string validationFailureMessage);
    }
}
