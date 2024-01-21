using StarWarsTracker.Application.Requests.EventRequests.InsertEventRequests;

namespace StarWarsTracker.Api.Controllers
{
    public class EventController : BaseController
    {
        public EventController(IOrchestrator orchestrator) : base(orchestrator) { }

        [HttpPost("Event/InsertEvent")]
        public async Task InsertEvent(InsertEventRequest request) => await _orchestrator.ExecuteRequestAsync(request);
    }
}
