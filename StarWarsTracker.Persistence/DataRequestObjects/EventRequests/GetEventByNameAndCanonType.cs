namespace StarWarsTracker.Persistence.DataRequestObjects.EventRequests
{
    public class GetEventByNameAndCanonType : IDataFetch<Event_DTO>
    {
        #region Constructor

        public GetEventByNameAndCanonType(string name, int canonTypeId)
        {
            Name = name;

            CanonTypeId = canonTypeId;
        }

        #endregion

        #region Public Properties / SQL Parameters

        public string Name { get; set; }

        public int CanonTypeId { get; set; }

        #endregion

        #region Public IDataFetch Methods

        public object? GetParameters() => this;

        public string GetSql() => $"SELECT * FROM {TableName.Event} WHERE Name = @Name AND CanonTypeId = @CanonTypeId";

        #endregion
    }
}
