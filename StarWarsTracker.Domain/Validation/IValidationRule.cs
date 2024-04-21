namespace StarWarsTracker.Domain.Validation
{
    /// <summary>
    /// This interface defines the contract for a Validation Rule.
    /// This is implemented by classes that define logic for validation and the failureMessage to use for validation failures.
    /// </summary>
    public interface IValidationRule
    {
        /// <summary>
        /// Apply Validation Logic and return True/False depending on if the Validation passes.
        /// Puts out ValidationFailureMessage string.Empty when validation passes, or the reason if validation fails.
        /// </summary>
        /// <param name="validationFailureMessage">The string that will be empty if validation passes or contains a reason if validation fails.</param>
        /// <returns>True if validation passes. False if Validation Fails.</returns>
        public bool IsPassingRule(out string validationFailureMessage);
    }
}
