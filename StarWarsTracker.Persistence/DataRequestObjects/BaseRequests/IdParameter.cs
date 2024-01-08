namespace StarWarsTracker.Persistence.DataRequestObjects.BaseRequests
{
    /// <summary>
    /// This base request can be reused by any IDataRequest which will GetParameters() => { Id };
    /// </summary>
    public abstract class IdParameter : IDataRequest
    {
        public int Id { get; set; }

        public IdParameter(int id) => Id = id;

        public object? GetParameters() => new { Id };

        public abstract string GetSql();
    }
}
