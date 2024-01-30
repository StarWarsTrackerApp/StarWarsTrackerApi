namespace StarWarsTracker.Domain.Exceptions
{
    public class AlreadyExistsException : Exception
    {
        public AlreadyExistsException(string nameOfObjectAlreadyExisting, params (object Value, string NameOfField)[] conflicts)
        {
            NameOfObjectAlreadyExisting = nameOfObjectAlreadyExisting;

            Conflicts = conflicts.Select(c => $"{nameOfObjectAlreadyExisting} already exists with {c.NameOfField}: {c.Value}");
        }

        public readonly string NameOfObjectAlreadyExisting;

        public readonly IEnumerable<string> Conflicts;
    }
}
