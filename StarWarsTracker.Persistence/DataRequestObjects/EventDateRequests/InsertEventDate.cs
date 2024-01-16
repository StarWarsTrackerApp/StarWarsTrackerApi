namespace StarWarsTracker.Persistence.DataRequestObjects.EventDateRequests
{
    public class InsertEventDate : IDataExecute
    {
        public InsertEventDate(Guid eventId, int eventDateTypeId, int yearsSinceBattleOfYavin, int sequence)
        {
            EventGuid = eventId;
            EventDateTypeId = eventDateTypeId;
            YearsSinceBattleOfYavin = yearsSinceBattleOfYavin;
            Sequence = sequence;
        }

        public Guid EventGuid { get; set; }

        public int EventDateTypeId { get; set; }

        public int YearsSinceBattleOfYavin { get; set; }

        public int Sequence { get; set; }

        public object? GetParameters() => this;

        public string GetSql() => 
        @$"
            DECLARE @EventId INT = (SELECT Id FROM {TableName.Event} WHERE Guid = @EventGuid)

            INSERT INTO {TableName.EventDate} (  EventId,  EventDateTypeId,  YearsSinceBattleOfYavin,  Sequence )
                                       VALUES ( @EventId, @EventDateTypeId, @YearsSinceBattleOfYavin, @Sequence )
        ";
    }
}
