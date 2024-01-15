using StarWarsTracker.Domain.Models;

namespace StarWarsTracker.Domain.Validation.EventDateValidation
{
    /// <summary>
    /// This rule defines that a collection of EventDates must be a valid Grouping. Examples: 
    /// (Definitive), (DefinitiveStart, DefinitiveEnd), (DefinitiveStart, SpeculativeEnd, SpeculativeEnd), 
    /// (SpeculativeStart, SpeculativeStart, DefinitiveEnd), (SpeculateStart, SpeculativeStart, SpeculativeEnd, SpeculativeEnd)
    /// </summary>
    public class EventDatesValidationRule : ValidationRule<IEnumerable<EventDate>>
    {
        public EventDatesValidationRule(IEnumerable<EventDate> objectToValidate, string nameOfObjectToValidate) : base(objectToValidate, nameOfObjectToValidate) { }

        public override bool IsPassingRule(out string validationFailureMessage)
        {
            throw new NotImplementedException();

            if (ObjectToValidate == null || !ObjectToValidate.Any())
            {
                validationFailureMessage = ValidationFailureMessage.RequiredField(NameOfObjectToValidate);
                return false;
            }

            if (ObjectToValidate.Count() > 4)
            {
                // validationFailureMessage = ValidationFailureMessage.
            }

            if (ObjectToValidate.Count() == 1)
            {
                var eventDate = ObjectToValidate.First();

                if(eventDate.EventDateType == Enums.EventDateType.Definitive)
                {
                    validationFailureMessage = string.Empty;
                    return true;
                }
            }

            if (ObjectToValidate.Count() == 2)
            {
                
            }

            validationFailureMessage = string.Empty;
            return false;
        }
    }
}
