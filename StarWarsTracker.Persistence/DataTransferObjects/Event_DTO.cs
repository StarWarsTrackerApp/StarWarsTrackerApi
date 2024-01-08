namespace StarWarsTracker.Persistence.DataTransferObjects
{
    public class Event_DTO
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public bool IsCannon { get; set; }
    }
}
