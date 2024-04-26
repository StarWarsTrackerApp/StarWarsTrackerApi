namespace StarWarsTracker.Domain.Exceptions
{
    public class DoesNotExistResponse : CustomExceptionResponse
    {
        public DoesNotExistResponse(string nameOfObject, params (object? Value, string NameOfField)[] valuesSearchedBy)
        {
            NameOfObject = nameOfObject;
            ValuesSearchedBy = valuesSearchedBy.Any() 
                             ? valuesSearchedBy.Select(_ => new KeyValuePair<string, object?>(_.NameOfField, _.Value)) 
                             : Enumerable.Empty<KeyValuePair<string, object?>>();
        }

        public string NameOfObject { get; set; }

        public IEnumerable<KeyValuePair<string, object?>> ValuesSearchedBy { get; set; }
    }
}
