using StarWarsTracker.Domain.Enums;

namespace StarWarsTracker.Domain.Models
{
    public class Event
    {
        public Event() { }

        public Event(Guid guid, string name, string description, CanonType canonType)
        {
            Guid = guid;
            Name = name;
            Description = description;
            CanonType = canonType;
        }

        public Guid Guid { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public CanonType CanonType { get; set; }
    }
}
