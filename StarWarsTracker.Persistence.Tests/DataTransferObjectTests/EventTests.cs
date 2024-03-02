using StarWarsTracker.Domain.Enums;
using StarWarsTracker.Persistence.DataTransferObjects;

namespace StarWarsTracker.Persistence.Tests.DataTransferObjectTests
{
    public class EventTests
    {
        [Fact]
        public void EventDTO_AsDomainEvent_ShouldReturn_ExpectedProperties()
        {
            var eventDTO = new Event_DTO()
            {
                Id = 1,
                Guid = Guid.NewGuid(),
                Name = TestString.Random(),
                Description = TestString.Random(),
                CanonTypeId = (int)CanonType.CanonAndLegends
            };

            var result = eventDTO.AsDomainEvent();

            Assert.Equal(eventDTO.Guid, result.Guid);
            Assert.Equal(eventDTO.Name, result.Name);
            Assert.Equal(eventDTO.Description, result.Description);
            Assert.Equal(eventDTO.CanonTypeId, (int)result.CanonType);
        }
    }
}
