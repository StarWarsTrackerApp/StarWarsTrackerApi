using StarWarsTracker.Application.Requests.EventRequests.Delete;
using StarWarsTracker.Application.Requests.EventRequests.GetAllNotHavingDates;
using StarWarsTracker.Application.Requests.EventRequests.GetByGuid;
using StarWarsTracker.Application.Requests.EventRequests.GetByNameAndCanonType;
using StarWarsTracker.Application.Requests.EventRequests.GetByNameLike;
using StarWarsTracker.Application.Requests.EventRequests.GetByYear;
using StarWarsTracker.Application.Requests.EventRequests.Insert;
using StarWarsTracker.Domain.Logging;

namespace StarWarsTracker.Api.Controllers
{
    public class EventController : BaseController
    {
        public EventController(IOrchestrator orchestrator, ILogMessage logMessage) : base(orchestrator, logMessage) { }

        [HttpPost("Event/InsertEvent")]
        public async Task InsertEvent(InsertEventRequest request) => await ExecuteRequestAsync(request);

        [HttpDelete("Event/DeleteEventByGuid")]
        public async Task DeleteEvent(DeleteEventByGuidRequest request) => await ExecuteRequestAsync(request);

        [HttpGet("Event/GetAllEventsNotHavingDates")]
        public async Task<GetAllEventsNotHavingDatesResponse> GetAllEventsNotHavingDates(GetAllEventsNotHavingDatesRequest request) => await GetResponseAsync(request);

        [HttpGet("Event/GetEventByGuid")]
        public async Task<GetEventByGuidResponse> GetEventByGuid(GetEventByGuidRequest request) => await GetResponseAsync(request);

        [HttpGet("Event/GetEventByNameAndCanonType")]
        public async Task<GetEventByNameAndCanonTypeResponse> GetEventByNameAndCanonType(GetEventByNameAndCanonTypeRequest request) => await GetResponseAsync(request);

        [HttpGet("Event/GetEventsByNameLike")]
        public async Task<GetEventsByNameLikeResponse> GetEventsByNameLike(GetEventsByNameLikeRequest request) => await GetResponseAsync(request);

        [HttpGet("Event/GetEventsByYear")]
        public async Task<GetEventsByYearResponse> GetEventsByYear(GetEventsByYearRequest request) => await GetResponseAsync(request);
    }
}
