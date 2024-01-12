namespace StarWarsTracker.Domain.Exceptions
{
    public class DoesNotExistException : Exception
    {
        public DoesNotExistException(string nameOfObjectNotExisting, params(object? Value, string NameOfValue)[] valuesSearchedBy)
        {
            NameOfObjectNotExisting = nameOfObjectNotExisting;

            ValuesSearchedBy = valuesSearchedBy.Select(_ => $"{_.NameOfValue} : {_.Value}");
        }

        public readonly string NameOfObjectNotExisting;

        public readonly IEnumerable<string> ValuesSearchedBy = Enumerable.Empty<string>();
    }
}
