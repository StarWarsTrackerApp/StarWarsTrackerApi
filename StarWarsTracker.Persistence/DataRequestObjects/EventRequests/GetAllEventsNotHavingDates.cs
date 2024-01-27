namespace StarWarsTracker.Persistence.DataRequestObjects.EventRequests
{
    public class GetAllEventsNotHavingDates : IDataFetch<Event_DTO>
    {
        public object? GetParameters() => null;

        public string GetSql() =>  $"SELECT * FROM {TableName.Event} WHERE NOT EXISTS (SELECT 1 FROM {TableName.EventDate} WHERE EventId = {TableName.Event}.Id)";
    }
}
