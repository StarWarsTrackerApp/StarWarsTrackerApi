using StarWarsTracker.Application.Requests.EventDateRequests.Delete;
using StarWarsTracker.Application.Requests.EventDateRequests.Insert;

namespace StarWarsTracker.Api.Controllers
{
    public class EventDateController : BaseController
    {
        public EventDateController(IOrchestrator orchestrator) : base(orchestrator) { }

        [HttpPost("EventDate/InsertEventDates")]
        public async Task InsertEventDates([FromBody] InsertEventDatesRequest request) => await _orchestrator.ExecuteRequestAsync(request);

        [HttpDelete("EventDate/DeleteEventDatesByEventGuid")]
        public async Task DeleteEventDatesByEventGuid(DeleteEventDatesByEventGuidRequest request) => await _orchestrator.ExecuteRequestAsync(request);
    }
}
