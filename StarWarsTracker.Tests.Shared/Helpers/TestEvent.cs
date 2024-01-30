using StarWarsTracker.Domain.Enums;
using StarWarsTracker.Persistence.Abstraction;
using StarWarsTracker.Persistence.DataRequestObjects.EventRequests;
using StarWarsTracker.Persistence.DataTransferObjects;

namespace StarWarsTracker.Tests.Shared.Helpers;

public  static class TestEvent
{
    private static IDataAccess _dataAccess = TestDataAccess.SharedInstance;

    /// <summary>
    /// Return a new InsertEvent DataRequestObject. Guid defaults to Guid.NewGuid() while Name and Description default to StringHelper.RandomString() and isCanon defaults to false
    /// </summary>
    public static InsertEvent NewInsertEvent(Guid? guid = null, string name = null!, string description = null!, CanonType canonType = CanonType.CanonAndLegends)
    {
        return new InsertEvent(
            guid ?? Guid.NewGuid(),
            name ?? TestString.Random(), 
            description ?? TestString.Random(), 
            (int)canonType
        );
    }

    /// <summary>
    /// Inserts an Event into database and then fetch and return the EventDTO.
    /// Uses EventHelper.NewInsertEvent() where Guid defaults to Guid.NewGuid() while Name and Description default to StringHelper.RandomString() and isCanon defaults to false
    /// </summary>
    public static async Task<Event_DTO> InsertAndFetchEventAsync(Guid? guid = null, string name = null!, string description = null!, CanonType canonType = CanonType.CanonAndLegends)
    {
        var insertRequest = NewInsertEvent(guid, name, description, canonType);

        await _dataAccess.ExecuteAsync(insertRequest);

        var eventInserted = await _dataAccess.FetchAsync(new GetEventByGuid(insertRequest.Guid));

        return eventInserted!;
    }
}
