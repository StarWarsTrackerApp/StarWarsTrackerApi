namespace StarWarsTracker.Persistence.DataRequestObjects.EventRequests
{
    public class InsertEvent : IDataExecute
    {
        public InsertEvent(string name, string description, bool isCanon)
        {
            Name = name;
            Description = description;
            IsCanon = isCanon;
        }

        public string Name { get; set; }

        public string Description { get; set; } 

        public bool IsCanon { get; set; }

        public object? GetParameters() => this;

        public string GetSql() => 
        @$"
            IF NOT EXISTS ( SELECT 1 FROM {TableName.Event} WHERE Name = @Name )
            BEGIN
                INSERT INTO {TableName.Event} (  Name,  Description,  IsCanon ) 
                                       VALUES ( @Name, @Description, @IsCanon )
            END
        ";
    }
}
