using StarWarsTracker.Domain.Enums;

namespace StarWarsTracker.Domain.Models
{
    public class EventDate : IEquatable<EventDate>
    {
        #region Constructors

        public EventDate() { }

        public EventDate(EventDateType eventDateType, int yearsSinceBattleOfYavin, int sequence)
        {
            EventDateType = eventDateType;
            YearsSinceBattleOfYavin = yearsSinceBattleOfYavin;
            Sequence = sequence;
        }

        #endregion

        #region Properties

        public EventDateType EventDateType { get; set; }

        public int YearsSinceBattleOfYavin { get; set; }

        public int Sequence { get; set; }

        #endregion

        #region Operator Overloads

        public static bool operator >(EventDate left, EventDate right) => left.YearsSinceBattleOfYavin > right.YearsSinceBattleOfYavin
                                                                       || (left.YearsSinceBattleOfYavin == right.YearsSinceBattleOfYavin && left.Sequence > right.Sequence);
        public static bool operator <(EventDate left, EventDate right) => left != right && !(left > right);

        public static bool operator ==(EventDate left, EventDate right) => left.YearsSinceBattleOfYavin == right.YearsSinceBattleOfYavin && left.Sequence == right.Sequence;

        public static bool operator !=(EventDate left, EventDate right) => !(left == right);

        public override bool Equals(object? obj) => obj != null && Equals((EventDate)obj);

        public override int GetHashCode() => HashCode.Combine(YearsSinceBattleOfYavin, Sequence);

        public bool Equals(EventDate? other) => other is not null && other.EventDateType == EventDateType && other.YearsSinceBattleOfYavin == YearsSinceBattleOfYavin && other.Sequence == Sequence;

        #endregion
    }
}
