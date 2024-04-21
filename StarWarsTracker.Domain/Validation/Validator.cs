namespace StarWarsTracker.Domain.Validation
{
    /// <summary>
    /// Validator class that is used to apply IValidationRules to IValidatable objects.
    /// </summary>
    public class Validator
    {
        #region Private Members

        /// <summary>
        /// The list of reasons that the validator has failed Validation based on IValidationRules that have been applied.
        /// </summary>
        private readonly List<string> _validationFailureReasons = new();

        #endregion

        #region Constructors 

        /// <summary>
        /// Constructor for Validator which applies IValidationRules that are passed in.
        /// </summary>
        /// <param name="rules">The IValidationRules to apply.</param>
        public Validator(params IValidationRule[] rules)
        {
            ApplyRules(rules);
        }
        
        #endregion

        #region Public Members

        /// <summary>
        /// Return a List of the Reasons the Validator has Failed Validation based on IValidationRules that have been applied.
        /// </summary>
        public List<string> ReasonsForFailure => _validationFailureReasons;

        /// <summary>
        /// Return True/False depending on if and IValidationRules that have been applied have failed.
        /// </summary>
        public bool IsPassingAllRules => _validationFailureReasons.Count == 0;

        /// <summary>
        /// Apply a single IValidationRule. If the validation fails, add to list of ReasonsForFailure
        /// </summary>
        /// <param name="validationRule">The IValidationRule to apply.</param>
        public void ApplyRule(IValidationRule validationRule)
        {
            if (!validationRule.IsPassingRule(out var validationFailureMessage))
            {
                _validationFailureReasons.Add(validationFailureMessage);
            }
        }

        /// <summary>
        /// Apply multiple IValidationRules. If any IValidationRules fail, add the reason to the list of ReasonsForFailure.
        /// </summary>
        /// <param name="rules">The IValidationRules to apply.</param>
        public void ApplyRules(params IValidationRule[] rules)
        {
            foreach (var rule in rules)
            {
                ApplyRule(rule);
            }
        }

        #endregion
    }
}
