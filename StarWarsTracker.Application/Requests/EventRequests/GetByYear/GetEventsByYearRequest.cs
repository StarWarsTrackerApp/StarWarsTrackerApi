namespace StarWarsTracker.Application.Requests.EventRequests.GetByYear
{
    public class GetEventsByYearRequest : IRequestResponse<GetEventsByYearResponse>
    {
        public GetEventsByYearRequest() { }

        /// <summary>
        /// The year you are looking for based on the Battle Of Yavin.
        /// Positive Numbers are AfterBattleYavin and Negative numbers are BeforeBattleYavin.
        /// </summary>
        /// <example>-19</example>
        public int YearsSinceBattleOfYavin { get; set; }
    }
}
