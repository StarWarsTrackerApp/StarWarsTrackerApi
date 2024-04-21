namespace StarWarsTracker.Persistence.DataRequestObjects.EventRequests
{
    public class GetEventsByYear : IDataFetch<Event_DTO>
    {
        #region Constructor

        public GetEventsByYear(int yearsSinceBattleOfYavin) => YearsSinceBattleOfYavin = yearsSinceBattleOfYavin;

        #endregion

        #region Public Properties / SQL Parameters

        public int YearsSinceBattleOfYavin { get; set; }

        #endregion

        #region Public IDataFetch Methods

        public object? GetParameters() => new { YearsSinceBattleOfYavin };

        public string GetSql() => 
        @$"
            SELECT * FROM {TableName.Event} 
                JOIN {TableName.EventDate} ON {TableName.Event}.Id = {TableName.EventDate}.EventId
            WHERE YearsSinceBattleOfYavin = @YearsSinceBattleOfYavin
            ORDER BY Sequence
        ";
        
        #endregion
    }
}
