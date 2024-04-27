using StarWarsTracker.Api.Examples.Events;
using StarWarsTracker.Api.SwaggerHelpers.Attributes;
using StarWarsTracker.Application.BaseObjects.ExceptionResponses;
using StarWarsTracker.Application.Requests.EventRequests.Delete;
using StarWarsTracker.Application.Requests.EventRequests.GetAllNotHavingDates;
using StarWarsTracker.Application.Requests.EventRequests.GetByGuid;
using StarWarsTracker.Application.Requests.EventRequests.GetByNameAndCanonType;
using StarWarsTracker.Application.Requests.EventRequests.GetByNameLike;
using StarWarsTracker.Application.Requests.EventRequests.GetByYear;
using StarWarsTracker.Application.Requests.EventRequests.Insert;
using StarWarsTracker.Domain.Constants.Routes;
using StarWarsTracker.Logging.Abstraction;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace StarWarsTracker.Api.Controllers
{
    public class EventController : BaseController
    {
        public EventController(IOrchestrator orchestrator, IClassLoggerFactory loggerFactory) : base(orchestrator, loggerFactory) { }

        #region Insert Event Endpoint 

        [HttpPost(EventRoute.Insert),
            SwaggerOperation("Insert a Star Wars Event into the database."),
            SwaggerRequestExample(typeof(InsertEventRequest), typeof(InsertEventExample.ValidRequest)),
            SuccessResponse(),
            ValidationFailedResponse(), 
            ValidationFailedExample(typeof(InsertEventExample.BadRequest)),
            AlreadyExistsResponse(), 
            AlreadyExistsExample(typeof(InsertEventExample.AlreadyExists))]
        public async Task InsertEvent([FromBody] InsertEventRequest request) => await ExecuteRequestAsync(request);

        #endregion

        #region Delete Event Endpoint

        [HttpDelete(EventRoute.Delete),
            SwaggerOperation("Delete a Star Wars Event using the EventGuid Identifier."),
            SuccessResponse(),
            ValidationFailedResponse(), 
            ValidationFailedExample(typeof(DeleteEventExample.BadRequest)),
            DoesNotExistResponse(), 
            DoesNotExistExample(typeof(DeleteEventExample.DoesNotExist))]
        public async Task DeleteEvent(DeleteEventByGuidRequest request) => await ExecuteRequestAsync(request);

        #endregion

        #region Get All Events Not Having Dates Endpoint

        [HttpGet(EventRoute.GetNotHavingDates),
            SwaggerOperation("Get all Star Wars Events that do not have an EventDate saved."),
            SuccessResponse(typeof(GetAllEventsNotHavingDatesResponse)), 
            SuccessResponseExample(typeof(GetAllEventsNotHavingDatesExample.SuccessResponse))]
        public async Task<GetAllEventsNotHavingDatesResponse> GetAllEventsNotHavingDates() => await GetResponseAsync(new GetAllEventsNotHavingDatesRequest());

        #endregion

        #region Get Event By Guid Endpoint

        [HttpGet(EventRoute.GetByGuid),
            SwaggerOperation("Get a specific Event and its Timeline using the EventGuid Identifier."),
            SuccessResponse(typeof(GetEventByGuidResponse)), 
            SuccessResponseExample(typeof(GetEventByGuidExample.SuccessResponse)),
            ValidationFailedResponse(), 
            ValidationFailedExample(typeof(GetEventByGuidExample.BadRequest)),
            DoesNotExistResponse(), 
            DoesNotExistExample(typeof(GetEventByGuidExample.DoesNotExist))]
        public async Task<GetEventByGuidResponse> GetEventByGuid(GetEventByGuidRequest request) => await GetResponseAsync(request);

        #endregion

        #region Get Event By Name And Canon Type Endpoint

        [HttpGet(EventRoute.GetByNameAndCanonType),
            SwaggerOperation("Search for a specific Event by specifying the Event's Name and the CanonType"),
            SuccessResponse(typeof(GetEventByNameAndCanonTypeResponse)), 
            SuccessResponseExample(typeof(GetEventByNameAndCanonTypeExample.SuccessResponse)),
            ValidationFailedResponse(), 
            ValidationFailedExample(typeof(GetEventByNameAndCanonTypeExample.BadRequest)),
            DoesNotExistResponse(), 
            DoesNotExistExample((typeof(GetEventByNameAndCanonTypeExample.DoesNotExist)))]
        public async Task<GetEventByNameAndCanonTypeResponse> GetEventByNameAndCanonType(GetEventByNameAndCanonTypeRequest request) => await GetResponseAsync(request);

        #endregion

        #region Get Events By Name Like Endpoint

        [HttpGet(EventRoute.GetByNameLike),
            SwaggerOperation("Get a list of Events where the name contains the Name provided."),
            SuccessResponse(typeof(GetEventsByNameLikeResponse)), 
            SuccessResponseExample(typeof(GetEventsByNameLikeExample.SuccessResponse)),
            ValidationFailedResponse(), 
            ValidationFailedExample(typeof(GetEventsByNameLikeExample.BadRequest)),
            DoesNotExistResponse(), 
            DoesNotExistExample(typeof(GetEventsByNameLikeExample.DoesNotExist))]
        public async Task<GetEventsByNameLikeResponse> GetEventsByNameLike(GetEventsByNameLikeRequest request) => await GetResponseAsync(request);

        #endregion

        #region Get Events By Year Endpoint

        [HttpGet(EventRoute.GetByYear),
            SwaggerOperation("Get a list of Events that happen during a specific year."),
            SuccessResponse(typeof(GetEventsByYearResponse)), 
            SuccessResponseExample(typeof(GetEventsByYearExample.SuccessResponse)),
            DoesNotExistResponse(), 
            DoesNotExistExample(typeof(GetEventsByYearExample.DoesNotExist))]
        public async Task<GetEventsByYearResponse> GetEventsByYear(GetEventsByYearRequest request) => await GetResponseAsync(request);

        #endregion

    }
}
