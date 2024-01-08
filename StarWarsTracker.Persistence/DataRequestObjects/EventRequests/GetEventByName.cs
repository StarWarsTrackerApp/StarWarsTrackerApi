namespace StarWarsTracker.Persistence.DataRequestObjects.EventRequests
{
    public class GetEventByName : IDataFetch<Event_DTO>
    {
        public GetEventByName(string name) => Name = name;

        public string Name { get; set; }

        public object? GetParameters() => this;

        public string GetSql() => $"SELECT * FROM {TableName.Event} WHERE Name = @Name";
    }
}
