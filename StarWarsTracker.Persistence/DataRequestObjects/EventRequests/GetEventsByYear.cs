namespace StarWarsTracker.Persistence.DataRequestObjects.EventRequests
{
    public class GetEventsByYear : IDataFetch<Event_DTO>
    {
        public GetEventsByYear(int yearsSinceBattleOfYavin) => YearsSinceBattleOfYavin = yearsSinceBattleOfYavin;

        public int YearsSinceBattleOfYavin { get; set; }

        public object? GetParameters() => new { YearsSinceBattleOfYavin };

        public string GetSql() => 
        @$"
            SELECT * FROM {TableName.Event} 
                JOIN {TableName.EventDate} ON {TableName.Event}.Id = {TableName.EventDate}.EventId
            WHERE YearsSinceBattleOfYavin = @YearsSinceBattleOfYavin
            ORDER BY Sequence
        ";
    }
}
