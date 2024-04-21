using StarWarsTracker.Application.Requests.EventRequests.Delete;
using StarWarsTracker.Application.Requests.EventRequests.GetAllNotHavingDates;
using StarWarsTracker.Application.Requests.EventRequests.GetByGuid;
using StarWarsTracker.Application.Requests.EventRequests.GetByNameAndCanonType;
using StarWarsTracker.Application.Requests.EventRequests.GetByNameLike;
using StarWarsTracker.Application.Requests.EventRequests.GetByYear;
using StarWarsTracker.Application.Requests.EventRequests.Insert;
using StarWarsTracker.Domain.Constants.Routes;
using StarWarsTracker.Logging.Abstraction;

namespace StarWarsTracker.Api.Controllers
{
    public class EventController : BaseController
    {
        public EventController(IOrchestrator orchestrator, IClassLoggerFactory loggerFactory) : base(orchestrator, loggerFactory) { }

        [HttpPost(EventRoute.Insert)]
        public async Task InsertEvent(InsertEventRequest request) => await ExecuteRequestAsync(request);

        [HttpDelete(EventRoute.Delete)]
        public async Task DeleteEvent(DeleteEventByGuidRequest request) => await ExecuteRequestAsync(request);

        [HttpGet(EventRoute.GetNotHavingDates)]
        public async Task<GetAllEventsNotHavingDatesResponse> GetAllEventsNotHavingDates(GetAllEventsNotHavingDatesRequest request) => await GetResponseAsync(request);

        [HttpGet(EventRoute.GetByGuid)]
        public async Task<GetEventByGuidResponse> GetEventByGuid(GetEventByGuidRequest request) => await GetResponseAsync(request);

        [HttpGet(EventRoute.GetByNameAndCanonType)]
        public async Task<GetEventByNameAndCanonTypeResponse> GetEventByNameAndCanonType(GetEventByNameAndCanonTypeRequest request) => await GetResponseAsync(request);

        [HttpGet(EventRoute.GetByNameLike)]
        public async Task<GetEventsByNameLikeResponse> GetEventsByNameLike(GetEventsByNameLikeRequest request) => await GetResponseAsync(request);

        [HttpGet(EventRoute.GetByYear)]
        public async Task<GetEventsByYearResponse> GetEventsByYear(GetEventsByYearRequest request) => await GetResponseAsync(request);
    }
}
