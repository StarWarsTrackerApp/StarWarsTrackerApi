using StarWarsTracker.Domain.Enums;
using StarWarsTracker.Persistence.Abstraction;
using StarWarsTracker.Persistence.DataRequestObjects.EventRequests;
using StarWarsTracker.Persistence.DataTransferObjects;
using StarWarsTracker.Tests.Shared.Helpers;

namespace StarWarsTracker.Persistence.Tests.TestHelpers
{
    internal static class EventHelper
    {
        /// <summary>
        /// Return a new InsertEvent DataRequestObject. Guid defaults to Guid.NewGuid() while Name and Description default to StringHelper.RandomString() and isCanon defaults to false
        /// </summary>
        public static InsertEvent NewInsertEvent(Guid? guid = null, string name = null!, string description = null!, CanonType canonType = CanonType.CanonAndLegends)
        {
            return new InsertEvent(
                guid ?? Guid.NewGuid(),
                name ?? StringHelper.RandomString(), 
                description ?? StringHelper.RandomString(), 
                (int)canonType
            );
        }

        /// <summary>
        /// Using the IDataAccess provided, will insert an Event into database and then fetch and return the EventDTO.
        /// Uses EventHelper.NewInsertEvent() where Guid defaults to Guid.NewGuid() while Name and Description default to StringHelper.RandomString() and isCanon defaults to false
        /// </summary>
        public static async Task<Event_DTO> InsertAndFetchEventAsync(IDataAccess dataAccess, Guid? guid = null, string name = null!, string description = null!, CanonType canonType = CanonType.CanonAndLegends)
        {
            var insertRequest = NewInsertEvent(guid, name, description, canonType);

            await dataAccess.ExecuteAsync(insertRequest);

            var eventInserted = await dataAccess.FetchAsync(new GetEventByGuid(insertRequest.Guid));

            return eventInserted!;
        }
    }
}
