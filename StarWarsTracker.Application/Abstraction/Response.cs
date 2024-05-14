using StarWarsTracker.Application.BaseObjects.BaseResponses;
using StarWarsTracker.Application.BaseObjects.ExceptionResponses;
using System.Net;

namespace StarWarsTracker.Application.Abstraction
{
    public static class Response
    {
        public static IResponse Success<T>(T responseContent) => 
            new GetResponse<T>((int)HttpStatusCode.OK, responseContent);

        public static IResponse Success() => 
            new ExecuteResponse((int)HttpStatusCode.OK);

        public static IResponse Error(string message = "Unexpected Error") => 
            new ErrorResponse(message);

        public static IResponse NotFound(string nameOfObject, params (object? Value, string NameOfField)[] valuesSearchedBy) => 
            new NotFoundResponse(nameOfObject, valuesSearchedBy);

        public static IResponse AlreadyExists(string nameOfObject, params (object Value, string NameOfField)[] possibleConflicts) =>
            new AlreadyExistsResponse(nameOfObject, possibleConflicts);

        public static IResponse ValidationFailure(params string[] validationFailureReasons) =>
            new ValidationFailureResponse(validationFailureReasons);

        public static IResponse ValidationFailure(IEnumerable<string> validationFailureReasons) =>
            new ValidationFailureResponse(validationFailureReasons);
    }
}
