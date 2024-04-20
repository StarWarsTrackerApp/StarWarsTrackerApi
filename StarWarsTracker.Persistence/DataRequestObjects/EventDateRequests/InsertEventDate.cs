namespace StarWarsTracker.Persistence.DataRequestObjects.EventDateRequests
{
    public class InsertEventDate : IDataExecute
    {
        #region Constructor

        public InsertEventDate(Guid eventGuid, int eventDateTypeId, int yearsSinceBattleOfYavin, int sequence)
        {
            EventGuid = eventGuid;
            EventDateTypeId = eventDateTypeId;
            YearsSinceBattleOfYavin = yearsSinceBattleOfYavin;
            Sequence = sequence;
        }

        #endregion

        #region Public Properties / SQL Parameters

        public Guid EventGuid { get; set; }

        public int EventDateTypeId { get; set; }

        public int YearsSinceBattleOfYavin { get; set; }

        public int Sequence { get; set; }

        #endregion

        #region Public IDataExecute Methods

        public object? GetParameters() => this;

        public string GetSql() => 
        @$"
            DECLARE @EventId INT = (SELECT Id FROM {TableName.Event} WHERE Guid = @EventGuid)

            INSERT INTO {TableName.EventDate} (  EventId,  EventDateTypeId,  YearsSinceBattleOfYavin,  Sequence )
                                       VALUES ( @EventId, @EventDateTypeId, @YearsSinceBattleOfYavin, @Sequence )
        ";

        #endregion
    }
}
