namespace StarWarsTracker.Persistence.DataRequestObjects.EventRequests
{
    public class GetEventByNameAndCanonType : IDataFetch<Event_DTO>
    {
        public GetEventByNameAndCanonType(string name, int canonTypeId)
        {
            Name = name;
            CanonTypeId = canonTypeId;
        }

        public string Name { get; set; }

        public int CanonTypeId { get; set; }

        public object? GetParameters() => this;

        public string GetSql() => $"SELECT * FROM {TableName.Event} WHERE Name = @Name AND CanonTypeId = @CanonTypeId";
    }
}
