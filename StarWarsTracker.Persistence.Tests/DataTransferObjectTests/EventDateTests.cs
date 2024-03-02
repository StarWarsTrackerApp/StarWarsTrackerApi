using StarWarsTracker.Domain.Enums;
using StarWarsTracker.Persistence.DataTransferObjects;

namespace StarWarsTracker.Persistence.Tests.DataTransferObjectTests
{
    public class EventDateTests
    {
        [Fact]
        public void EventDateDTO_AsDomainEventDate_ShouldReturn_ExpectedProperties()
        {
            var eventDateDTO = new EventDate_DTO()
            {
                Id = 1,
                EventId = 2,
                EventDateTypeId = (int)EventDateType.DefinitiveEnd,
                YearsSinceBattleOfYavin = 3,
                Sequence = 4
            };

            var result = eventDateDTO.AsDomainEventDate();

            Assert.Equal(eventDateDTO.EventDateTypeId, (int)result.EventDateType);
            Assert.Equal(eventDateDTO.YearsSinceBattleOfYavin, result.YearsSinceBattleOfYavin);
            Assert.Equal(eventDateDTO.Sequence, result.Sequence);
        }
    }
}
