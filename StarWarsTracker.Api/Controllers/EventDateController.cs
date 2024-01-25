using StarWarsTracker.Application.Requests.EventDateRequests.Insert;

namespace StarWarsTracker.Api.Controllers
{
    public class EventDateController : BaseController
    {
        public EventDateController(IOrchestrator orchestrator) : base(orchestrator) { }

        [HttpPost("EventDate/InsertEventTimeFrame")]
        public async Task InsertEventTimeFrame([FromBody] InsertEventTimeFrameRequest request) => await _orchestrator.ExecuteRequestAsync(request);
    }
}
