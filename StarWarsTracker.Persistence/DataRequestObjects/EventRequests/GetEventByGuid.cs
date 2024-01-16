namespace StarWarsTracker.Persistence.DataRequestObjects.EventRequests
{
    public class GetEventByGuid : IDataFetch<Event_DTO>
    {
        public GetEventByGuid(Guid guid) => Guid = guid;

        public Guid Guid { get; set; }

        public object? GetParameters() => this;

        public string GetSql() => $"SELECT * FROM {TableName.Event} WHERE Guid = @Guid";
    }
}
