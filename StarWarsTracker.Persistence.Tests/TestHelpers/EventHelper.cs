using StarWarsTracker.Domain.Enums;
using StarWarsTracker.Persistence.Abstraction;
using StarWarsTracker.Persistence.DataRequestObjects.EventRequests;
using StarWarsTracker.Persistence.DataTransferObjects;

namespace StarWarsTracker.Persistence.Tests.TestHelpers
{
    internal static class EventHelper
    {
        /// <summary>
        /// Return a new InsertEvent DataRequestObject. Name and Description default to StringHelper.RandomString() and isCanon defaults to false
        /// </summary>
        public static InsertEvent NewInsertEvent(string name = null!, string description = null!, CanonType canonType = CanonType.CanonAndLegends)
        {
            return new InsertEvent(name ?? StringHelper.RandomString(), description ?? StringHelper.RandomString(), (int)canonType);
        }

        /// <summary>
        /// Using the IDataAccess provided, will insert an Event into database and then fetch and return the EventDTO.
        /// Uses EventHelper.NewInsertEvent() which has Name and Description default to StringHelper.RandomString() and isCanon defaults to false
        /// </summary>
        public static async Task<Event_DTO> InsertAndFetchEventAsync(IDataAccess dataAccess, string name = null!, string description = null!, CanonType canonType = CanonType.CanonAndLegends)
        {
            var insertRequest = NewInsertEvent(name, description, canonType);

            await dataAccess.ExecuteAsync(insertRequest);

            var eventInserted = await dataAccess.FetchAsync(new GetEventByName(insertRequest.Name));

            return eventInserted!;
        }
    }
}
