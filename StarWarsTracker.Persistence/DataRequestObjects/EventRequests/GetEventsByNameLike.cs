namespace StarWarsTracker.Persistence.DataRequestObjects.EventRequests
{
    public class GetEventsByNameLike : IDataFetch<Event_DTO>
    {
        #region Constructor

        public GetEventsByNameLike(string name) =>  Name = name;

        #endregion

        #region Public Properties / SQL Parameters

        public string Name { get; set; }

        #endregion

        #region Public IDataFetch Methods

        public object? GetParameters() => new { Name = $"%{Name}%" };

        public string GetSql() => $"SELECT * FROM {TableName.Event} WHERE Name LIKE @Name";
        
        #endregion
    }
}
