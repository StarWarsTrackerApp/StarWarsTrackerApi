namespace StarWarsTracker.Domain.Validation
{
    /// <summary>
    /// This base class can be reused for IValidationRules that are going to use both the ObjectToValidate and the Name of the ObjectToValidate.
    /// </summary>
    /// <typeparam name="TypeToValidate">The Type of object that is being validated</typeparam>
    public abstract class ValidationRule<TypeToValidate> : IValidationRule
    {
        #region Constructor

        /// <summary>
        /// Constructor for a new ValidationRule which uses the ObjectToValidate and the Name of the ObjectToValidate.
        /// </summary>
        /// <param name="objectToValidate">The object that is being validated</param>
        /// <param name="nameOfObjectToValidate">The name of the object that is being validated.</param>
        public ValidationRule(TypeToValidate objectToValidate, string nameOfObjectToValidate)
        {
            ObjectToValidate = objectToValidate;

            NameOfObjectToValidate = nameOfObjectToValidate;
        }

        #endregion

        #region Public Properties

        public TypeToValidate ObjectToValidate { get; set; }

        public string NameOfObjectToValidate { get; set; }

        #endregion

        #region Public IValidationRule Method

        public abstract bool IsPassingRule(out string validationFailureMessage);
        
        #endregion
    }
}
