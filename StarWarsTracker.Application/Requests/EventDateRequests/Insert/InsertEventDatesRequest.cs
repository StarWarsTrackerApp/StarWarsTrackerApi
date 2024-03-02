using StarWarsTracker.Domain.Validation.EventDateValidation;
using StarWarsTracker.Domain.Validation.GuidValidation;

namespace StarWarsTracker.Application.Requests.EventDateRequests.Insert
{
    public class InsertEventDatesRequest : IRequest, IValidatable
    {
        public InsertEventDatesRequest() { }

        public InsertEventDatesRequest(Guid eventGuid, EventDate[] eventDates)
        {
            EventGuid = eventGuid;
            EventDates = eventDates;
        }

        public Guid EventGuid { get; set; }

        public EventDate[] EventDates { get; set; } = Array.Empty<EventDate>();

        public bool IsValid(out Validator validator)
        {
            validator = new Validator(
                new GuidRequiredRule(EventGuid, nameof(EventGuid)),
                new EventDatesValidTimeFrameRule(EventDates, nameof(EventDates))
            );

            return validator.IsPassingAllRules;
        }
    }
}
