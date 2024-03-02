using System.Diagnostics.CodeAnalysis;

namespace StarWarsTracker.Domain.Exceptions
{
    [ExcludeFromCodeCoverage]
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
