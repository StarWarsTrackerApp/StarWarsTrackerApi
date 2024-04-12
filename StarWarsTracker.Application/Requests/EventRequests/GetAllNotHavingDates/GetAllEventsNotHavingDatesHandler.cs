using StarWarsTracker.Logging.Abstraction;
using StarWarsTracker.Persistence.DataRequestObjects.EventRequests;

namespace StarWarsTracker.Application.Requests.EventRequests.GetAllNotHavingDates
{
    internal class GetAllEventsNotHavingDatesHandler : DataRequestResponseHandler<GetAllEventsNotHavingDatesRequest, GetAllEventsNotHavingDatesResponse>
    {
        public GetAllEventsNotHavingDatesHandler(IDataAccess dataAccess, IClassLoggerFactory loggerFactory) : base(dataAccess, loggerFactory) { }

        public override async Task<GetAllEventsNotHavingDatesResponse> GetResponseAsync(GetAllEventsNotHavingDatesRequest request)
        {
            var events = await _dataAccess.FetchListAsync(new GetAllEventsNotHavingDates());

            if (events.Any())
            {
                _logger.AddDebug("Events Found", events);

                var response = new GetAllEventsNotHavingDatesResponse(events.Select(_ => _.AsDomainEvent()));

                return response;
            }

            _logger.AddInfo("No Events Found Without Dates");

            return new GetAllEventsNotHavingDatesResponse();
        }
    }
}
