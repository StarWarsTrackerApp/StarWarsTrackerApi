namespace StarWarsTracker.Persistence.DataRequestObjects.EventRequests
{
    public class InsertEvent : IDataExecute
    {
        public InsertEvent(string name, string description, int canonTypeId)
        {
            Name = name;
            Description = description;
            CanonTypeId = canonTypeId;
        }

        public string Name { get; set; }

        public string Description { get; set; } 

        public int CanonTypeId { get; set; }

        public object? GetParameters() => this;

        public string GetSql() => 
        @$"
            IF NOT EXISTS ( SELECT 1 FROM {TableName.Event} WHERE Name = @Name AND CanonTypeId = @CanonTypeId )
            BEGIN
                INSERT INTO {TableName.Event} (  Name,  Description,  CanonTypeId ) 
                                       VALUES ( @Name, @Description, @CanonTypeId )
            END
        ";
    }
}
