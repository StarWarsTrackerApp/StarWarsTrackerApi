using StarWarsTracker.Domain.Enums;
using StarWarsTracker.Domain.Models;

namespace StarWarsTracker.Persistence.DataTransferObjects
{
    public class EventDate_DTO
    {
        public int Id { get; set; }

        public int EventId { get; set; }

        public int EventDateTypeId { get; set; }

        public int YearsSinceBattleOfYavin { get; set; }

        public int Sequence { get; set; }

        public EventDate AsDomainEventDate() => new((EventDateType)EventDateTypeId, YearsSinceBattleOfYavin, Sequence);
    }
}
