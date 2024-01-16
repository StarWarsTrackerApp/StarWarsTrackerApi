namespace StarWarsTracker.Persistence.DataTransferObjects
{
    public class Event_DTO
    {
        public int Id { get; set; }

        public Guid Guid { get; set; } = Guid.Empty;

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public int CanonTypeId { get; set; }
    }
}
