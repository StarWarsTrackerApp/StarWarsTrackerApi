using StarWarsTracker.Domain.Constants;
using StarWarsTracker.Domain.Enums;

namespace StarWarsTracker.Domain.Models
{
    public class EventTimeFrame
    {
        #region Constructors

        public EventTimeFrame(IEnumerable<EventDate> eventDates) : this(eventDates.ToArray()) { }

        public EventTimeFrame(params EventDate[] eventDates)
        {
            _eventDates = eventDates.OrderBy(_ => _.YearsSinceBattleOfYavin).ThenBy(_ => _.Sequence).ToArray();

            switch (eventDates.Length)
            {
                case 1: SetTimeFrameTypeForSingleDate();
                    break;

                case 2: SetTimeFrameTypeForTwoDates();
                    break;

                case 3: SetTimeFrameTypeForThreeDates();
                    break;

                case 4: SetTimeFrameTypeForFourDates();
                    break;

                default: SetTimeFrameTypeToInvalid();
                    break;
            }
        }

        #endregion

        #region Private Fields

        private EventDate[] _eventDates;

        private EventTimeFrameType _timeFrameType;

        private string _invalidFormattingNotes = string.Empty;

        #endregion

        #region Exposed Members

        public EventDate[] EventDates => _eventDates;

        public EventTimeFrameType TimeFrameType => _timeFrameType;

        public bool IsValidTimeFrame(out string invalidFormattingNotes)
        {
            invalidFormattingNotes = _invalidFormattingNotes;

            return _timeFrameType != EventTimeFrameType.Invalid;
        }

        #endregion

        #region Private Helpers For Setting TimeFrameType During Initialization

        /// <summary>
        /// Helper to set TimeFrameType when TimeFrame has a single EventDate (Should be DefinitiveTime TimeFrameType)
        /// </summary>
        private void SetTimeFrameTypeForSingleDate()
        {
            var eventDate = _eventDates.Single();

            if (eventDate.EventDateType != EventDateType.Definitive)
            {
                SetTimeFrameTypeToInvalid();
            }
            else
            {
                _timeFrameType = EventTimeFrameType.DefinitiveTime;
            }
        }

        /// <summary>
        /// Helper to set TimeFrameType when TimeFrame has two EventDates (Should be DefinitiveStartDefinitiveEnd TimeFrameType)
        /// </summary>
        private void SetTimeFrameTypeForTwoDates()
        {
            var startingDate = _eventDates.FirstOrDefault(_ => _.EventDateType == EventDateType.DefinitiveStart);
            var endingDate = _eventDates.FirstOrDefault(_ => _.EventDateType == EventDateType.DefinitiveEnd);

            if (startingDate is null || endingDate is null)
            {
                SetTimeFrameTypeToInvalid();
                return;
            }

            if (startingDate < endingDate)
            {
                _timeFrameType = EventTimeFrameType.DefinitiveStartDefinitiveEnd;
                return;
            }

            SetTimeFrameTypeToInvalid(EventTimeFrameType.DefinitiveStartDefinitiveEnd);
        }

        /// <summary>
        /// Helper to set TimeFrameType when TimeFrame has three EventDates (Should be DefinitiveStartSpeculativeEnd or SpeculativeStartDefinitiveEnd TimeFrameType)
        /// </summary>
        private void SetTimeFrameTypeForThreeDates()
        {
            var startingDates = _eventDates.Where(_ => _.EventDateType == EventDateType.DefinitiveStart || _.EventDateType == EventDateType.SpeculativeStart);
            var isDefinitiveStart = startingDates.Count() == 1 && startingDates.First().EventDateType == EventDateType.DefinitiveStart;

            var endingDates = _eventDates.Where(_ => _.EventDateType == EventDateType.DefinitiveEnd || _.EventDateType == EventDateType.SpeculativeEnd);
            var isDefinitiveEnd = endingDates.Count() == 1 && endingDates.First().EventDateType == EventDateType.DefinitiveEnd;               

            if (isDefinitiveStart && !isDefinitiveEnd)
            {
                if (endingDates.Count() == 2 && endingDates.All(_ => _.EventDateType == EventDateType.SpeculativeEnd))
                {
                    var startDate = startingDates.First();
                    var endDateA = endingDates.ElementAt(0);
                    var endDateB = endingDates.ElementAt(1);

                    if (startDate < endDateA && startDate < endDateB && endDateA != endDateB)
                    {
                        _timeFrameType = EventTimeFrameType.DefinitiveStartSpeculativeEnd;
                        return;
                    }
                }

                SetTimeFrameTypeToInvalid(EventTimeFrameType.DefinitiveStartSpeculativeEnd);
                return;
            }

            if (isDefinitiveEnd && !isDefinitiveStart)
            {
                if(startingDates.Count() == 2 && startingDates.All(_ => _.EventDateType == EventDateType.SpeculativeStart))
                {
                    var endDate = endingDates.First();
                    var startDateA = startingDates.ElementAt(0);
                    var startDateB = startingDates.ElementAt(1);

                    if (startDateA < endDate && startDateB < endDate && startDateA != startDateB)
                    {
                        _timeFrameType = EventTimeFrameType.SpeculativeStartDefinitiveEnd;
                        return;
                    }
                }

                SetTimeFrameTypeToInvalid(EventTimeFrameType.SpeculativeStartDefinitiveEnd);
                return;
            }

            SetTimeFrameTypeToInvalid();
        }
        
        /// <summary>
        /// Helper to set TimeFrameType when TimeFrame has four EventDates (Should be SpeculativeStartAndSpeculativeEnd TimeFrameType)
        /// </summary>
        private void SetTimeFrameTypeForFourDates()
        {
            var startingDates = _eventDates.Where(_ => _.EventDateType == EventDateType.SpeculativeStart);
            var endingDates = _eventDates.Where(_ => _.EventDateType == EventDateType.SpeculativeEnd);

            if (startingDates.Count() == 2 && endingDates.Count() == 2)
            {
                var startDateA = startingDates.ElementAt(0);
                var startDateB = startingDates.ElementAt(1);

                var endDateA = endingDates.ElementAt(0);
                var endDateB = endingDates.ElementAt(1);

                if (startDateA == startDateB || endDateA == endDateB)
                {
                    SetTimeFrameTypeToInvalid(EventTimeFrameType.SpeculativeStartSpeculativeEnd);
                    return;
                }

                if (startDateA < endDateA && startDateA < endDateB && startDateB < endDateA && startDateB < endDateB)
                {
                    _timeFrameType = EventTimeFrameType.SpeculativeStartSpeculativeEnd;
                    return;
                }

                SetTimeFrameTypeToInvalid(EventTimeFrameType.SpeculativeStartSpeculativeEnd);
                return;
            }

            SetTimeFrameTypeToInvalid();
        }

        /// <summary>
        /// Helper for setting TimeFrameType to invalid and setting appropriate InvalidFormattingNotes based on the TimeFrameType provided.
        /// </summary>
        private void SetTimeFrameTypeToInvalid(EventTimeFrameType timeFrameTypeFormatting = EventTimeFrameType.Invalid)
        {
            _timeFrameType = EventTimeFrameType.Invalid;
            _invalidFormattingNotes = EventTimeFrameFormatting.GetFormattingRules(timeFrameTypeFormatting);
        }

        #endregion
    }
}