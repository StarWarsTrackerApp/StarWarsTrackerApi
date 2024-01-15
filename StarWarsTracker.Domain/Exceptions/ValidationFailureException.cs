namespace StarWarsTracker.Domain.Exceptions
{
    public class ValidationFailureException : Exception
    {
        public ValidationFailureException() { }

        public ValidationFailureException(IEnumerable<string> validationFailureMessages)
        {
            ValidationFailureMessages = validationFailureMessages;
        }

        public IEnumerable<string> ValidationFailureMessages { get; set; } = Enumerable.Empty<string>();
    }
}
