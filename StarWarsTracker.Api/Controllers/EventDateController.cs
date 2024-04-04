using StarWarsTracker.Application.Requests.EventDateRequests.Delete;
using StarWarsTracker.Application.Requests.EventDateRequests.Insert;
using StarWarsTracker.Domain.Logging;

namespace StarWarsTracker.Api.Controllers
{
    public class EventDateController : BaseController
    {
        public EventDateController(IOrchestrator orchestrator, ILogMessage logMessage) : base(orchestrator, logMessage) { }

        [HttpPost("EventDate/InsertEventDates")]
        public async Task InsertEventDates([FromBody] InsertEventDatesRequest request) => await ExecuteRequestAsync(request);

        [HttpDelete("EventDate/DeleteEventDatesByEventGuid")]
        public async Task DeleteEventDatesByEventGuid(DeleteEventDatesByEventGuidRequest request) => await ExecuteRequestAsync(request);
    }
}
