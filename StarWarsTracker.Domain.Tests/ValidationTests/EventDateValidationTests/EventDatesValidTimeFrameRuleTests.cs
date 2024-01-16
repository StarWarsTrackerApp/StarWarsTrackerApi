using StarWarsTracker.Domain.Enums;
using StarWarsTracker.Domain.Models;
using StarWarsTracker.Domain.Validation.EventDateValidation;

namespace StarWarsTracker.Domain.Tests.ValidationTests.EventDateValidationTests
{
    public class EventDatesValidTimeFrameRuleTests
    {
        public static IEnumerable<object[]> ValidTimeFrames = new List<object[]>
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

        [Theory]
        [MemberData(nameof(ValidTimeFrames))]
        public void EventDatesValidTimeFrameRule_Given_TimeFrameIsValid_IsPassingRule_ShouldReturn_True(EventDate[] eventDates)
        {
            var rule = new EventDatesValidTimeFrameRule(eventDates, nameof(eventDates));

            var isValid = rule.IsPassingRule(out _);

            Assert.True(isValid);
        }

        public static IEnumerable<object[]> InvalidTimeFrames = new List<object[]>
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

        [Theory]
        [MemberData(nameof(InvalidTimeFrames))]
        public void EventDatesValidTimeFrameRule_Given_TimeFrameIsNotValid_IsPassingRule_ShouldReturn_False(EventDate[] eventDates)
        {
            var rule = new EventDatesValidTimeFrameRule(eventDates, nameof(eventDates));

            var isValid = rule.IsPassingRule(out _);

            Assert.False(isValid);
        }
    }
}
