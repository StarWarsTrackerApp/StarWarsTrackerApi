using StarWarsTracker.Application.Requests.EventDateRequests.Insert;
using StarWarsTracker.Domain.Enums;
using StarWarsTracker.Domain.Models;

namespace StarWarsTracker.Application.Tests.RequestTests.EventDateRequestTests.InsertEventDatesTests
{
    public class InsertEventDatesRequestTests
    {
        #region Event Dates For Test Cases

        public static IEnumerable<object[]> InvalidEventDates = new List<object[]>
        {
            new object[]
            {
                null!
            },
            new object[]
            {
                Enumerable.Empty<EventDate>()
            },
            new object[]
            {
                new [] { new EventDate(EventDateType.DefinitiveStart, 0, 0) }
            },
            new object[]
            {
                new[] { new EventDate(EventDateType.DefinitiveEnd, 0, 0) }
            },
            new object[]
            {
                new [] { new EventDate(EventDateType.SpeculativeStart, 0, 0) }
            },
            new object[]
            {
                new[] { new EventDate(EventDateType.SpeculativeEnd, 0, 0) }
            },
            new object[]
            {
                new[] { new EventDate(EventDateType.SpeculativeEnd, 0, 0) }
            },
            new object[]
            {
                new[]
                {
                    new EventDate(EventDateType.DefinitiveStart, 0, 0),
                    new EventDate(EventDateType.DefinitiveEnd, 0, 0)
                }
            },
            new object[]
            {
                new[]
                {
                    new EventDate(EventDateType.SpeculativeStart, 1, 0),
                    new EventDate(EventDateType.SpeculativeEnd, 2, 0)
                }
            },
            new object[]
            {
                new[]
                {
                    new EventDate(EventDateType.SpeculativeStart, 1, 0),
                    new EventDate(EventDateType.SpeculativeStart, 1, 0),
                    new EventDate(EventDateType.DefinitiveEnd, 2, 10)
                }
            },
            new object[]
            {
                new[]
                {
                    new EventDate(EventDateType.DefinitiveStart, 1, 0),
                    new EventDate(EventDateType.SpeculativeEnd, 1, 0),
                    new EventDate(EventDateType.SpeculativeEnd, 1, 1)
                }
            },
            new object[]
            {
                new[]
                {
                    new EventDate(EventDateType.SpeculativeStart, 1, 5),
                    new EventDate(EventDateType.SpeculativeStart, 1, 5),
                    new EventDate(EventDateType.SpeculativeEnd, 2, 10),
                    new EventDate(EventDateType.SpeculativeEnd, 2, 20)
                }
            },
            new object[]
            {
                new[]
                {
                    new EventDate(EventDateType.SpeculativeStart, 1, 5),
                    new EventDate(EventDateType.SpeculativeStart, 1, 10),
                    new EventDate(EventDateType.SpeculativeEnd, 2, 10),
                    new EventDate(EventDateType.SpeculativeEnd, 2, 10)
                }
            },
        };

        public static IEnumerable<object[]> ValidEventDates = new List<object[]>
        {
            new object[]
            {
                new [] { new EventDate(EventDateType.Definitive, 0, 0) }
            },
            new object[]
            {
                new[]
                {
                    new EventDate(EventDateType.DefinitiveStart, 0, 0),
                    new EventDate(EventDateType.DefinitiveEnd, 1, 0)
                }
            },
            new object[]
            {
                new[]
                {
                    new EventDate(EventDateType.DefinitiveStart, 1, 0),
                    new EventDate(EventDateType.SpeculativeEnd, 2, 5),
                    new EventDate(EventDateType.SpeculativeEnd, 2, 10)
                }
            },
            new object[]
            {
                new []
                {
                    new EventDate(EventDateType.SpeculativeStart, 1, 0),
                    new EventDate(EventDateType.SpeculativeStart, 2, 5),
                    new EventDate(EventDateType.DefinitiveEnd, 2, 10)
                }
            },
            new object[]
            {
                new[]
                {
                    new EventDate(EventDateType.SpeculativeStart, 1, 5),
                    new EventDate(EventDateType.SpeculativeStart, 1, 10),
                    new EventDate(EventDateType.SpeculativeEnd, 2, 10),
                    new EventDate(EventDateType.SpeculativeEnd, 2, 20)
                }
            }
        };

        #endregion

        [Theory]
        [MemberData(nameof(ValidEventDates))]
        public void InsertEventDatesRequest_Given_EmptyGuid_WithValidEventDates_IsValid_ShouldReturn_False(EventDate[] validEventDates)
        {
            var request = new InsertEventDatesRequest(Guid.Empty, validEventDates);

            Assert.False(request.IsValid(out _));
        }
      
        [Theory]
        [MemberData(nameof(InvalidEventDates))]
        public void InsertEventDatesRequest_Given_ValidGuid_WithInvalidEventDates_IsValid_ShouldReturn_False(EventDate[] invalidEventDates)
        {
            var request = new InsertEventDatesRequest(Guid.NewGuid(), invalidEventDates);

            Assert.False(request.IsValid(out _));
        }

        [Theory]
        [MemberData(nameof(InvalidEventDates))]
        public void InsertEventDatesRequest_Given_EmptyGuidAndInvalidEventDates_IsValid_ShouldReturn_False(EventDate[] invalidEventDates)
        {
            var request = new InsertEventDatesRequest(Guid.Empty, invalidEventDates);

            Assert.False(request.IsValid(out _));
        }

        [Theory]
        [MemberData(nameof(ValidEventDates))]
        public void InsertEventDatesRequest_Given_ValidGuidAndValidEventDates_ShouldReturn_True(EventDate[] validEventDates)
        {
            var request = new InsertEventDatesRequest(Guid.NewGuid(), validEventDates);

            Assert.True(request.IsValid(out _));
        }
    }
}
