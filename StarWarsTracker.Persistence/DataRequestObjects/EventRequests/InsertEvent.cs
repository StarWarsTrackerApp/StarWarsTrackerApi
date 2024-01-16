namespace StarWarsTracker.Persistence.DataRequestObjects.EventRequests
{
    public class InsertEvent : IDataExecute
    {
        public InsertEvent(Guid guid, string name, string description, int canonTypeId)
        {
            Guid = guid;
            Name = name;
            Description = description;
            CanonTypeId = canonTypeId;
        }

        public Guid Guid { get; set; }

        public string Name { get; set; }

        public string Description { get; set; } 

        public int CanonTypeId { get; set; }

        public object? GetParameters() => this;

        public string GetSql() => 
        @$"
            IF NOT EXISTS ( SELECT 1 FROM {TableName.Event} WHERE Guid = @Guid )
            BEGIN
                INSERT INTO {TableName.Event} (  Guid,  Name,  Description,  CanonTypeId ) 
                                       VALUES ( @Guid, @Name, @Description, @CanonTypeId )
            END
        ";
    }
}
