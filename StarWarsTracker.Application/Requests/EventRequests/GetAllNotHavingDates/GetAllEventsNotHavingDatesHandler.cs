using StarWarsTracker.Domain.Logging;
using StarWarsTracker.Persistence.DataRequestObjects.EventRequests;

namespace StarWarsTracker.Application.Requests.EventRequests.GetAllNotHavingDates
{
    internal class GetAllEventsNotHavingDatesHandler : DataRequestResponseHandler<GetAllEventsNotHavingDatesRequest, GetAllEventsNotHavingDatesResponse>
    {
        public GetAllEventsNotHavingDatesHandler(IDataAccess dataAccess, ILogMessage logMessage) : base(dataAccess, logMessage) { }

        public override async Task<GetAllEventsNotHavingDatesResponse> GetResponseAsync(GetAllEventsNotHavingDatesRequest request)
        {
            var events = await _dataAccess.FetchListAsync(new GetAllEventsNotHavingDates());

            if (events.Any())
            {
                _logMessage.AddDebug(this, "Events Found", events);

                var response = new GetAllEventsNotHavingDatesResponse(events.Select(_ => _.AsDomainEvent()));

                return response;
            }

            _logMessage.AddInfo(this, "No Events Found Without Dates");

            return new GetAllEventsNotHavingDatesResponse();
        }
    }
}
