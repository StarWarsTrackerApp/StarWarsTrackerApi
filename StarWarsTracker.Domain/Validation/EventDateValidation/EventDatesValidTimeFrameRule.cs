using StarWarsTracker.Domain.Models;

namespace StarWarsTracker.Domain.Validation.EventDateValidation
{
    /// <summary>
    /// This rule defines that a collection of EventDates must be a valid Grouping. Examples: 
    /// (Definitive), (DefinitiveStart, DefinitiveEnd), (DefinitiveStart, SpeculativeEnd, SpeculativeEnd), 
    /// (SpeculativeStart, SpeculativeStart, DefinitiveEnd), (SpeculateStart, SpeculativeStart, SpeculativeEnd, SpeculativeEnd)
    /// </summary>
    public class EventDatesValidTimeFrameRule : ValidationRule<IEnumerable<EventDate>>
    {
        public EventDatesValidTimeFrameRule(IEnumerable<EventDate> objectToValidate, string nameOfObjectToValidate) : base(objectToValidate, nameOfObjectToValidate) { }

        public override bool IsPassingRule(out string validationFailureMessage)
        {
            if (ObjectToValidate == null || !ObjectToValidate.Any())
            {
                validationFailureMessage = ValidationFailureMessage.RequiredField(NameOfObjectToValidate);
                return false;
            }

            var timeFrame = new EventTimeFrame(ObjectToValidate.ToArray());

            if (timeFrame.IsValidTimeFrame(out var invalidFormattingNotes))
            {
                validationFailureMessage = string.Empty;
                return true;
            }

            validationFailureMessage = ValidationFailureMessage.BadFormat(ObjectToValidate, NameOfObjectToValidate, invalidFormattingNotes);
            return false;
        }
    }
}
