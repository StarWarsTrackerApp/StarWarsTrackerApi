using StarWarsTracker.Application.Abstraction;
using StarWarsTracker.Application.Requests.EventRequests;

namespace StarWarsTracker.Api.Controllers
{
    public class EventController : BaseController
    {
        public EventController(IOrchestrator orchestrator) : base(orchestrator) { }

        [HttpPost("Event/InsertEvent")]
        public async Task InsertEvent(InsertEventRequest request)
        {
            await _orchestrator.SendRequest(request);
        }

    }
}
