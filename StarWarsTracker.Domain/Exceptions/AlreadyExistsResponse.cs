using StarWarsTracker.Domain.Exceptions;

namespace StarWarsTracker.Application.BaseObjects.ExceptionResponses
{
    public class AlreadyExistsResponse : CustomExceptionResponse
    {
        public AlreadyExistsResponse(string nameOfObject, params (object Value, string NameOfField)[] possibleConflicts)
        {
            ObjectAlreadyExisting = nameOfObject;

            PossibleConflicts = possibleConflicts.Any() ? possibleConflicts.Select( _ => new { Field = _.NameOfField, _.Value }) : Enumerable.Empty<object>();
        }

        public string ObjectAlreadyExisting { get; set; }

        public IEnumerable<object> PossibleConflicts { get; set; }
    }
}
