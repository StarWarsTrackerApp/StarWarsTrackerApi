namespace StarWarsTracker.Persistence.DataRequestObjects.EventRequests
{
    public class DeleteEventById : IdParameter, IDataExecute
    {
        public DeleteEventById(int id) : base(id) { }

        public override string GetSql() => $"DELETE FROM {TableName.Event} WHERE Id = @Id";
    }
}