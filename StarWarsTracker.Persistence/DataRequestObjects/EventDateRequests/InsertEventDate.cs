namespace StarWarsTracker.Persistence.DataRequestObjects.EventDateRequests
{
    public class InsertEventDate : IDataExecute
    {
        public InsertEventDate(int eventId, int eventDateTypeId, int yearsSinceBattleOfYavin, int sequence)
        {
            EventId = eventId;
            EventDateTypeId = eventDateTypeId;
            YearsSinceBattleOfYavin = yearsSinceBattleOfYavin;
            Sequence = sequence;
        }

        public int EventId { get; set; }

        public int EventDateTypeId { get; set; }

        public int YearsSinceBattleOfYavin { get; set; }

        public int Sequence { get; set; }

        public object? GetParameters() => this;

        public string GetSql() => 
        @$"
            INSERT INTO {TableName.EventDate} (  EventId,  EventDateTypeId,  YearsSinceBattleOfYavin,  Sequence )
                                       VALUES ( @EventId, @EventDateTypeId, @YearsSinceBattleOfYavin, @Sequence )
        ";
    }
}
