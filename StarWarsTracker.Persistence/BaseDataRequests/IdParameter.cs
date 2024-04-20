namespace StarWarsTracker.Persistence.BaseDataRequests
{
    /// <summary>
    /// This base request can be reused by any IDataRequest which will GetParameters() => { Id };
    /// </summary>
    public abstract class IdParameter : IDataRequest
    {
        #region Constructor

        public IdParameter(int id) => Id = id;

        #endregion

        #region Public Properties / SQL Parameters

        public int Id { get; set; }

        #endregion

        #region Public IDataRequest Methods

        public object? GetParameters() => new { Id };

        public abstract string GetSql();
        
        #endregion
    }
}
