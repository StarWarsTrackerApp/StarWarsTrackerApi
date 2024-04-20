namespace StarWarsTracker.Persistence.DataRequestObjects.EventRequests
{
    public class InsertEvent : IDataExecute
    {
        #region Constructor

        public InsertEvent(Guid guid, string name, string description, int canonTypeId)
        {
            Guid = guid;
            Name = name;
            Description = description;
            CanonTypeId = canonTypeId;
        }

        #endregion

        #region Public Properties / SQL Parameters

        public Guid Guid { get; set; }

        public string Name { get; set; }

        public string Description { get; set; } 

        public int CanonTypeId { get; set; }

        #endregion

        #region Public IDataExecute Methods

        public object? GetParameters() => this;

        public string GetSql() => 
        @$"
            IF NOT EXISTS ( SELECT 1 FROM {TableName.Event} WHERE Guid = @Guid )
            BEGIN
                INSERT INTO {TableName.Event} (  Guid,  Name,  Description,  CanonTypeId ) 
                                       VALUES ( @Guid, @Name, @Description, @CanonTypeId )
            END
        ";
        
        #endregion
    }
}
