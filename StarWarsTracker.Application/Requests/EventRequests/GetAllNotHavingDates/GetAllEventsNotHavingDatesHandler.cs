using StarWarsTracker.Persistence.DataRequestObjects.EventRequests;

namespace StarWarsTracker.Application.Requests.EventRequests.GetAllNotHavingDates
{
    internal class GetAllEventsNotHavingDatesHandler : DataRequestResponseHandler<GetAllEventsNotHavingDatesRequest, GetAllEventsNotHavingDatesResponse>
    {
        public GetAllEventsNotHavingDatesHandler(IDataAccess dataAccess) : base(dataAccess) { }

        public override async Task<GetAllEventsNotHavingDatesResponse> GetResponseAsync(GetAllEventsNotHavingDatesRequest request)
        {
            var events = await _dataAccess.FetchListAsync(new GetAllEventsNotHavingDates());

            if (events.Any())
            {
                return new GetAllEventsNotHavingDatesResponse(events.Select(_ => _.AsDomainEvent()));
            }

            return new GetAllEventsNotHavingDatesResponse();
        }
    }
}
