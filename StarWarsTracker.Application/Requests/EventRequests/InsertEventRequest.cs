using StarWarsTracker.Application.Abstraction;
using StarWarsTracker.Domain.Enums;

namespace StarWarsTracker.Application.Requests.EventRequests
{
    public class InsertEventRequest : IRequest
    {
        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public CanonType CanonType { get; set; }
    }
}
