using StarWarsTracker.Application.Requests.EventDateRequests.Delete;
using StarWarsTracker.Application.Requests.EventDateRequests.Insert;
using StarWarsTracker.Logging.Abstraction;

namespace StarWarsTracker.Api.Controllers
{
    public class EventDateController : BaseController
    {
        public EventDateController(IHandlerFactory handlerFactory, IClassLoggerFactory loggerFactory) : base(handlerFactory, loggerFactory) { }

        [HttpPost("EventDate/InsertEventDates")]
        public async Task<IActionResult> InsertEventDates([FromBody] InsertEventDatesRequest request) => await HandleAsync(request);

        [HttpDelete("EventDate/DeleteEventDatesByEventGuid")]
        public async Task<IActionResult> DeleteEventDatesByEventGuid(DeleteEventDatesByEventGuidRequest request) => await HandleAsync(request);
    }
}
