namespace StarWarsTracker.Domain.Exceptions
{
    public class OperationFailedResponse : CustomExceptionResponse
    {
        public string Message { get; set; } = "Unexpected Error";
    }
}
