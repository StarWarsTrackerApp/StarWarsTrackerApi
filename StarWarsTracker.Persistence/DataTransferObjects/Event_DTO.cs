using StarWarsTracker.Domain.Enums;
using StarWarsTracker.Domain.Models;

namespace StarWarsTracker.Persistence.DataTransferObjects
{
    /// <summary>
    /// DTO representing the Event table exactly.
    /// </summary>
    public class Event_DTO
    {
        public int Id { get; set; }

        public Guid Guid { get; set; } = Guid.Empty;

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public int CanonTypeId { get; set; }

        public Event AsDomainEvent() => new(Guid, Name, Description, (CanonType)CanonTypeId);
    }
}
