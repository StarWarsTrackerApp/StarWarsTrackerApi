using StarWarsTracker.Domain.Enums;

namespace StarWarsTracker.Domain.Models
{
    public class EventDate
    {
        public EventDateType EventDateType { get; set; }

        public int YearsSinceBattleOfYavin { get; set; }

        public int Sequence { get; set; }
    }
}
