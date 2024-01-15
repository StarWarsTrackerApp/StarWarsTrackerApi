using StarWarsTracker.Domain.Enums;

namespace StarWarsTracker.Domain.Validation.EnumValidation
{
    /// <summary>
    /// This rule defines that the CanonType objectToValidate must be a valid CanonType.
    /// </summary>
    public class RequiredCanonTypeRule : ValidationRule<CanonType>
    {
        public RequiredCanonTypeRule(CanonType objectToValidate, string nameOfObjectToValidate) : base(objectToValidate, nameOfObjectToValidate) { }

        public override bool IsPassingRule(out string validationFailureMessage)
        {
            if(Enum.IsDefined(typeof(CanonType), ObjectToValidate))
            {
                validationFailureMessage = string.Empty;
                return true;
            }

            validationFailureMessage = ValidationFailureMessage.InvalidValue(ObjectToValidate, NameOfObjectToValidate);
            return false;
        }
    }
}
