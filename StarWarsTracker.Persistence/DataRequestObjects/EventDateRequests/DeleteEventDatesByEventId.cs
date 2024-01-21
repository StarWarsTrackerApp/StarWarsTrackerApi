using StarWarsTracker.Persistence.BaseDataRequests;

namespace StarWarsTracker.Persistence.DataRequestObjects.EventDateRequests
{
    public class DeleteEventDatesByEventId : IdParameter, IDataExecute
    {
        public DeleteEventDatesByEventId(int id) : base(id) { }

        public override string GetSql() => $"DELETE FROM {TableName.EventDate} WHERE EventId = @Id";
    }
}
