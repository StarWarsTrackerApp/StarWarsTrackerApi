namespace StarWarsTracker.Domain.Validation
{
    public abstract class ValidationRule<TypeToValidate> : IValidationRule
    {
        public ValidationRule(TypeToValidate objectToValidate, string nameOfObjectToValidate)
        {
            ObjectToValidate = objectToValidate;

            NameOfObjectToValidate = nameOfObjectToValidate;
        }

        public TypeToValidate ObjectToValidate { get; set; }

        public string NameOfObjectToValidate { get; set; }

        public abstract bool IsPassingRule(out string validationFailureMessage);
    }
}
