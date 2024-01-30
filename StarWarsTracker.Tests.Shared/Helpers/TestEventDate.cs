using StarWarsTracker.Domain.Enums;
using StarWarsTracker.Persistence.Abstraction;
using StarWarsTracker.Persistence.DataRequestObjects.EventDateRequests;
using StarWarsTracker.Persistence.DataRequestObjects.EventRequests;
using StarWarsTracker.Persistence.DataTransferObjects;

namespace StarWarsTracker.Tests.Shared.Helpers
{
    public class TestEventDate
    {
        private static IDataAccess _dataAccess = TestDataAccess.SharedInstance;

        /// <summary>
        /// Returns the Event_DTO and all EventDate_DTO's associated with that event using the guid provided after inserting a new EventDate. 
        /// If guid is null or an event does not exist for the guid provided, then a new event is inserted with the guid provided or a random guid when null.
        /// EventDateType will default to Definitive, YearsSinceBattleOfYavin and Sequence will both default to 1.
        /// </summary>
        public static async Task<(Event_DTO, IEnumerable<EventDate_DTO>)> InsertAndFetchEventDateAsync(Guid? guid = null, EventDateType? eventDateType = null, int? yearsSincleBattleOfYavin = null, int? sequence = null)
        {
            Event_DTO eventDTO;

            if (guid == null)
            {
                eventDTO = await TestEvent.InsertAndFetchEventAsync();

                guid = eventDTO.Guid;
            }
            else
            {
                var existingEvent = await _dataAccess.FetchAsync(new GetEventByGuid(guid.Value));

                eventDTO = existingEvent ?? await TestEvent.InsertAndFetchEventAsync(guid);
            }

            eventDateType ??= EventDateType.Definitive;
            yearsSincleBattleOfYavin ??= 1;
            sequence ??= 1;

            await _dataAccess.ExecuteAsync(new InsertEventDate(guid.Value, (int)eventDateType, yearsSincleBattleOfYavin.Value, sequence.Value));

            var eventDatesDTO = await _dataAccess.FetchListAsync(new GetEventDatesByEventId(eventDTO.Id));

            return (eventDTO, eventDatesDTO);
        }
    }
}
