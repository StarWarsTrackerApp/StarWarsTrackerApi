namespace StarWarsTracker.Application.Requests.EventRequests.GetByYear
{
    public class GetEventsByYearRequest : IRequestResponse<GetEventsByYearResponse>
    {
        public int YearsSinceBattleOfYavin { get; set; }
    }
}
