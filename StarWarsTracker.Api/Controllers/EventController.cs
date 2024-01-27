using StarWarsTracker.Application.Requests.EventRequests.Delete;
using StarWarsTracker.Application.Requests.EventRequests.GetAllNotHavingDates;
using StarWarsTracker.Application.Requests.EventRequests.GetByGuid;
using StarWarsTracker.Application.Requests.EventRequests.GetByNameLike;
using StarWarsTracker.Application.Requests.EventRequests.GetByYear;
using StarWarsTracker.Application.Requests.EventRequests.Insert;

namespace StarWarsTracker.Api.Controllers
{
    public class EventController : BaseController
    {
        public EventController(IOrchestrator orchestrator) : base(orchestrator) { }

        [HttpPost("Event/InsertEvent")]
        public async Task InsertEvent(InsertEventRequest request) => await _orchestrator.ExecuteRequestAsync(request);

        [HttpDelete("Event/DeleteEventByGuid")]
        public async Task DeleteEvent(DeleteEventByGuidRequest request) => await _orchestrator.ExecuteRequestAsync(request);

        [HttpGet("Event/GetAllEventsNotHavingDates")]
        public async Task<GetAllEventsNotHavingDatesResponse> GetAllEventsNotHavingDates(GetAllEventsNotHavingDatesRequest request) => await _orchestrator.GetRequestResponseAsync(request);

        [HttpGet("Event/GetEventByGuid")]
        public async Task<GetEventByGuidResponse> GetEventByGuid(GetEventByGuidRequest request) => await _orchestrator.GetRequestResponseAsync(request);

        [HttpGet("Event/GetEventsByNameLike")]
        public async Task<GetEventsByNameLikeResponse> GetEventsByNameLike(GetEventsByNameLikeRequest request) => await _orchestrator.GetRequestResponseAsync(request);

        [HttpGet("Event/GetEventsByYear")]
        public async Task<GetEventsByYearResponse> GetEventsByYear(GetEventsByYearRequest request) => await _orchestrator.GetRequestResponseAsync(request);
    }
}
