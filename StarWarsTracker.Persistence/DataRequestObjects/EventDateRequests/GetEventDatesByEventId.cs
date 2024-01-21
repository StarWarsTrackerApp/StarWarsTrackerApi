using StarWarsTracker.Persistence.BaseDataRequests;

namespace StarWarsTracker.Persistence.DataRequestObjects.EventDateRequests
{
    public class GetEventDatesByEventId : IdParameter, IDataFetch<EventDate_DTO>
    {
        public GetEventDatesByEventId(int id) : base(id) { }

        public override string GetSql() => $"SELECT * FROM {TableName.EventDate} WHERE EventId = @Id";
    }
}
