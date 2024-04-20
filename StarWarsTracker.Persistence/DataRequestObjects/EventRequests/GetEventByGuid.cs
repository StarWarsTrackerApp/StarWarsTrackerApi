namespace StarWarsTracker.Persistence.DataRequestObjects.EventRequests
{
    public class GetEventByGuid : IDataFetch<Event_DTO>
    {
        #region Constructor 

        public GetEventByGuid(Guid guid) => Guid = guid;

        #endregion

        #region Public Properties / SQL Parameters

        public Guid Guid { get; set; }

        #endregion

        #region Public IDataFetch Methods 

        public object? GetParameters() => this;

        public string GetSql() => $"SELECT * FROM {TableName.Event} WHERE Guid = @Guid";

        #endregion
    }
}
