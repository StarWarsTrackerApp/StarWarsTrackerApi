namespace StarWarsTracker.Persistence.DataRequestObjects.EventRequests
{
    public class GetEventsByNameLike : IDataFetch<Event_DTO>
    {
        public GetEventsByNameLike(string name) =>  Name = name;

        public string Name { get; set; }

        public object? GetParameters() => new { Name = $"%{Name}%" };

        public string GetSql() => $"SELECT * FROM {TableName.Event} WHERE Name LIKE @Name";
    }
}
