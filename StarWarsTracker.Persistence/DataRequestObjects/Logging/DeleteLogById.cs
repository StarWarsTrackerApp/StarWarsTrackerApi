using StarWarsTracker.Persistence.BaseDataRequests;

namespace StarWarsTracker.Persistence.DataRequestObjects.Logging
{
    public class DeleteLogById : IdParameter, IDataExecute
    {
        public DeleteLogById(int id) : base(id) { }

        public override string GetSql() => $"DELETE FROM {TableName.Log} WHERE Id = @Id";
    }
}
