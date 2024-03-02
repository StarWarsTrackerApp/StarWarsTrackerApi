namespace StarWarsTracker.Application.Requests.EventRequests.GetByYear
{
    public class GetEventsByYearRequest : IRequestResponse<GetEventsByYearResponse>
    {
        public GetEventsByYearRequest() { }

        public int YearsSinceBattleOfYavin { get; set; }
    }
}
