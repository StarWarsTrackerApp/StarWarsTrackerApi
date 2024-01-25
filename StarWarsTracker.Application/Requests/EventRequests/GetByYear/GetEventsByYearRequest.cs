namespace StarWarsTracker.Application.Requests.EventRequests.GetByYear
{
    public class GetEventsByYearRequest : IRequestResponse<GetEventsByYearResponse>
    {
        public GetEventsByYearRequest() { }

        public GetEventsByYearRequest(int yearsSinceBattleOfYavin)
        {
            YearsSinceBattleOfYavin = yearsSinceBattleOfYavin;
        }

        public int YearsSinceBattleOfYavin { get; set; }
    }
}
