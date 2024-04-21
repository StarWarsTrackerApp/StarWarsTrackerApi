namespace StarWarsTracker.Domain.Validation
{
    /// <summary>
    /// This class exposes methods for reuseable Validation Failure Messages with values interpolated into the message.
    /// </summary>
    public static class ValidationFailureMessage
    {
        /// <summary>
        /// Helper method for standardized message to use when a Required Field is missing.
        /// </summary>
        /// <param name="nameOfRequiredField">The name of the Required Field that is missing.</param>
        /// <returns>Standardized message for when a Required Field is missing.</returns>
        public static string RequiredField(string nameOfRequiredField) => $"Required field is missing. Name of required field: {nameOfRequiredField}";

        /// <summary>
        /// Helper method for standardized message to use when a string is Exceeding the Max Length it can be.
        /// </summary>
        /// <param name="value">The value that is exceeding max length.</param>
        /// <param name="nameOfField">The name of the value that is exceeding max length.</param>
        /// <param name="maxLength">The max length that has been exceeded.</param>
        /// <returns>Standardized message for when a string is exceeding the Max Length it can be.</returns>
        public static string StringExceedingMaxLength(string value, string nameOfField, int maxLength) => $"{nameOfField} field is exceeding Max Length of {maxLength}. Value received: {value}";

        /// <summary>
        /// Helper method for standardized message to use when an object is an invalid value.
        /// </summary>
        /// <param name="value">The object that is an invalid value.</param>
        /// <param name="nameOfField">The name of the object that is an invalid value.</param>
        /// <returns>Standardized message for when an object is an invalid value.</returns>
        public static string InvalidValue(object value, string nameOfField) => $"{nameOfField} is an invalid value. Value received: {value}";

        /// <summary>
        /// Helper method for standardized message to use when an object is a bad format.
        /// </summary>
        /// <param name="value">The object that is a bad format.</param>
        /// <param name="nameOfField">The name of the object that is a bad format.</param>
        /// <param name="formatIssue">The formatting issue that was wrong with the object.</param>
        /// <returns>Standardized message for when an object is a bad format.</returns>
        public static string BadFormat(object value, string nameOfField, string formatIssue) => $"{nameOfField} is in an incorrect format. {formatIssue} - Value received: {value}"; 
    }
}
