using StarWarsTracker.Domain.Enums;
using System.Text;

namespace StarWarsTracker.Domain.Constants
{
    public static class EventTimeFrameFormatting
    {
        public const string DefinitiveTime =
            "An EventTimeFrame with a Definitive Time must have a single EventDate with the EventDateType of Definitive.";

        public const string DefinitiveStartAndDefinitiveEnd =
            "An EventTimeFrame with a Definitive Start and Definitive End must have two EventDates with the EventDateTypes DefinitiveStart and DefinitiveEnd. " +
            "The DefinitiveStart must occur before the DefinitiveEnd.";

        public const string DefinitiveStartAndSpeculativeEnd =
            "An EventTimeFrame with a Definitive Start and Speculative End must have three EventDates, one with the EventDateType DefinitiveStart and two with SpeculativeEnd. " +
            "The DefinitiveStart must occur before the SpeculativeEnds which cannot be the same Year/Sequence.";

        public const string SpeculativeStartAndDefinitiveEnd =
            "An EventTimeFrame with a Speculative Start and Definitive End must have three EventDates, two with EventDateType SpeculativeStart and one with DefinitiveEnd. " +
            "The DefinitiveEnd must occur after the SpeculativeStarts which cannot be the same Year/Sequence.";

        public const string SpeculativeStartAndSpeculativeEnd =
            "An EventTimeFrame with a Speculative Start and Speculative End must have four EventDates, two with EventDateType SpeculativeStart and two with SpeculativeEnd. " +
            "The SpeculativeStarts cannot be the same Year/Sequence and must occur before the pair of SpeculativeEnds which also cannot be the same Year/Sequence.";

        /// <summary>
        /// Helper to get FormattingRules from EventTimeFrameFormatting.
        /// Defaults to EventTimeFrameType.Invalid which return all rules or returns rules specific to EventTimeFrameType if one is provided.
        /// </summary>
        public static string GetFormattingRules(EventTimeFrameType eventTimeFrameType = EventTimeFrameType.Invalid)
        {
            switch (eventTimeFrameType)
            {
                case EventTimeFrameType.DefinitiveTime:
                    return DefinitiveTime;

                case EventTimeFrameType.DefinitiveStartDefinitiveEnd:
                    return DefinitiveStartAndDefinitiveEnd;

                case EventTimeFrameType.DefinitiveStartSpeculativeEnd:
                    return DefinitiveStartAndSpeculativeEnd;

                case EventTimeFrameType.SpeculativeStartDefinitiveEnd:
                    return SpeculativeStartAndDefinitiveEnd;

                case EventTimeFrameType.SpeculativeStartSpeculativeEnd:
                    return SpeculativeStartAndSpeculativeEnd;

                default:
                case EventTimeFrameType.Invalid:
                    var sb = new StringBuilder();
                    sb.Append(DefinitiveTime);
                    sb.Append(" - ");
                    sb.Append(DefinitiveStartAndDefinitiveEnd);
                    sb.Append(" - ");
                    sb.Append(DefinitiveStartAndSpeculativeEnd);
                    sb.Append(" - ");
                    sb.Append(SpeculativeStartAndDefinitiveEnd);
                    sb.Append(" - ");
                    sb.Append(SpeculativeStartAndSpeculativeEnd);
                    return sb.ToString();
            }
        }
    }
}
