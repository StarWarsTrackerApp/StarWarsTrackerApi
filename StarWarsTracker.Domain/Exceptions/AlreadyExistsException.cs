namespace StarWarsTracker.Domain.Exceptions
{
    public class AlreadyExistsException : Exception
    {
        public AlreadyExistsException(string objectAlreadyExisting, params (object Value, string NameOfField)[] conflicts) =>
            Conflicts = conflicts.Select(c => $"{objectAlreadyExisting} already exists with {c.NameOfField}: {c.Value}");

        public readonly IEnumerable<string> Conflicts;
    }
}
