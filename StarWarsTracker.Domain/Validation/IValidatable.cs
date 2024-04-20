namespace StarWarsTracker.Domain.Validation
{
    /// <summary>
    /// This interface defines the contract for any object that has Validation rules. 
    /// </summary>
    public interface IValidatable
    {
        /// <summary>
        /// Checks to see if a request is valid. 
        /// Returns true/false depending on if the request passes the validation.
        /// Puts out the Validator used to checkin validation.
        /// </summary>
        /// <param name="validator">The validator used to process Validation Rules.</param>
        /// <returns>True if request passes validation. False if request fails validation.</returns>
        public bool IsValid(out Validator validator);
    }
}
