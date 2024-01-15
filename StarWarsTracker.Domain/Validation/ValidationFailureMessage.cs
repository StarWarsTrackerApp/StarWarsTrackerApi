namespace StarWarsTracker.Domain.Validation
{
    public static class ValidationFailureMessage
    {
        public static string RequiredField(string nameOfRequiredField) => $"Required field is missing. Name of required field: {nameOfRequiredField}";

        public static string StringExceedingMaxLength(string value, string nameOfField, int maxLength) => $"{nameOfField} field is exceeding Max Length of {maxLength}. Value received: {value}";

        public static string InvalidValue(object value, string nameOfField) => $"{nameOfField} is an invalid value. Value received: {value}";

        public static string BadFormat(object value, string nameOfField, string formatIssue) => $"{nameOfField} is in an incorrect format. {formatIssue} - Value received: {value}"; 
    }
}
