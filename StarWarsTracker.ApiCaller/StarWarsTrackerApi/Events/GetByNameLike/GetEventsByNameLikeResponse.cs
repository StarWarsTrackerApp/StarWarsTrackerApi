using StarWarsTracker.Domain.Models;

namespace StarWarsTracker.ApiCaller.StarWarsTrackerApi.Events.GetByNameLike
{
    public class GetEventsByNameLikeResponse
    {
        public GetEventsByNameLikeResponse(Event[] events)
        {
            Events = events;
        }

        public GetEventsByNameLikeResponse()
        {

        }

        public Event[] Events { get; set; } = Array.Empty<Event>(); 
    }

}