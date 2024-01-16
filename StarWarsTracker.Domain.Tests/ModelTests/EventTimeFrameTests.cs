using StarWarsTracker.Domain.Constants;
using StarWarsTracker.Domain.Enums;
using StarWarsTracker.Domain.Models;

namespace StarWarsTracker.Domain.Tests.ModelTests
{
    public class EventTimeFrameTests
    {
        #region EventDates Returned In Chronological Order Test

        [Fact]
        public void EventTimeFrame_EventDates_Given_EventDatesOutOfOrder_ShouldReturn_EventDates_InOrderByYearsThenSequence()
        {
            var firstDate = new EventDate(EventDateType.SpeculativeStart, 1, 10);
            var secondDate = new EventDate(EventDateType.SpeculativeStart, 2, 20);
            var thirdDate = new EventDate(EventDateType.SpeculativeEnd, 3, 30);
            var fourthDate = new EventDate(EventDateType.SpeculativeEnd, 4, 40);

            // initialize EventTimeFrame with EventDates out of order
            var timeFrame = new EventTimeFrame(fourthDate, secondDate, thirdDate, firstDate);

            var dates = timeFrame.EventDates;

            // Assert that the dates are in chronological order
            Assert.Equal(firstDate, dates[0]);
            Assert.Equal(secondDate, dates[1]);
            Assert.Equal(thirdDate, dates[2]);
            Assert.Equal(fourthDate, dates[3]);
        }

        #endregion

        #region TimeFrameType Tests

        public static IEnumerable<object[]> TimeFramesAndExpectedTimeFrameType = new List<object[]>
        {
            new object[] 
            { new EventTimeFrame(new EventDate(EventDateType.Definitive, 0, 0)), EventTimeFrameType.DefinitiveTime },
            new object[] 
            { new EventTimeFrame(new EventDate(EventDateType.DefinitiveStart, 0, 0)), EventTimeFrameType.Invalid },
            new object[] 
            { new EventTimeFrame(new EventDate(EventDateType.DefinitiveEnd, 0, 0)), EventTimeFrameType.Invalid },
            new object[] 
            { new EventTimeFrame(new EventDate(EventDateType.SpeculativeStart, 0, 0)), EventTimeFrameType.Invalid },
            new object[] 
            { new EventTimeFrame(new EventDate(EventDateType.SpeculativeEnd, 0, 0)), EventTimeFrameType.Invalid },
            new object[] 
            { new EventTimeFrame(new EventDate(EventDateType.SpeculativeEnd, 0, 0)), EventTimeFrameType.Invalid },
            new object[] 
            { new EventTimeFrame(
                new EventDate(EventDateType.DefinitiveStart, 0, 0),
                new EventDate(EventDateType.DefinitiveEnd, 1, 0)
                ), EventTimeFrameType.DefinitiveStartDefinitiveEnd 
            },
            new object[]
            { new EventTimeFrame(
                new EventDate(EventDateType.DefinitiveStart, 0, 0),
                new EventDate(EventDateType.DefinitiveEnd, 0, 0)
                ), EventTimeFrameType.Invalid
            },
            new object[]
            { new EventTimeFrame(
                new EventDate(EventDateType.SpeculativeStart, 1, 0),
                new EventDate(EventDateType.SpeculativeEnd, 2, 0)
                ), EventTimeFrameType.Invalid
            },
            new object[]
            { new EventTimeFrame(
                new EventDate(EventDateType.DefinitiveStart, 1, 0),
                new EventDate(EventDateType.SpeculativeEnd, 2, 5),
                new EventDate(EventDateType.SpeculativeEnd, 2, 10)
                ), EventTimeFrameType.DefinitiveStartSpeculativeEnd
            },
            new object[]
            { new EventTimeFrame(
                new EventDate(EventDateType.SpeculativeStart, 1, 0),
                new EventDate(EventDateType.SpeculativeStart, 2, 5),
                new EventDate(EventDateType.DefinitiveEnd, 2, 10)
                ), EventTimeFrameType.SpeculativeStartDefinitiveEnd
            },
            new object[]
            { new EventTimeFrame(
                new EventDate(EventDateType.SpeculativeStart, 1, 0),
                new EventDate(EventDateType.SpeculativeStart, 1, 0),
                new EventDate(EventDateType.DefinitiveEnd, 2, 10)
                ), EventTimeFrameType.Invalid
            },
            new object[]
            { new EventTimeFrame(
                new EventDate(EventDateType.DefinitiveStart, 1, 0),
                new EventDate(EventDateType.SpeculativeEnd, 1, 0),
                new EventDate(EventDateType.SpeculativeEnd, 1, 1)
                ), EventTimeFrameType.Invalid
            },
            new object[]
            { new EventTimeFrame(
                new EventDate(EventDateType.SpeculativeStart, 1, 5),
                new EventDate(EventDateType.SpeculativeStart, 1, 10),
                new EventDate(EventDateType.SpeculativeEnd, 2, 10),
                new EventDate(EventDateType.SpeculativeEnd, 2, 20)
                ), EventTimeFrameType.SpeculativeStartSpeculativeEnd
            },
            new object[]
            { new EventTimeFrame(
                new EventDate(EventDateType.SpeculativeStart, 1, 5),
                new EventDate(EventDateType.SpeculativeStart, 1, 5),
                new EventDate(EventDateType.SpeculativeEnd, 2, 10),
                new EventDate(EventDateType.SpeculativeEnd, 2, 20)
                ), EventTimeFrameType.Invalid
            },
            new object[]
            { new EventTimeFrame(
                new EventDate(EventDateType.SpeculativeStart, 1, 5),
                new EventDate(EventDateType.SpeculativeStart, 1, 10),
                new EventDate(EventDateType.SpeculativeEnd, 2, 10),
                new EventDate(EventDateType.SpeculativeEnd, 2, 10)
                ), EventTimeFrameType.Invalid
            },
        };

        [Theory]
        [MemberData(nameof(TimeFramesAndExpectedTimeFrameType))]
        public void EventTimeFrame_TimeFrameType_Given_DefinitiveEventDate_ShouldReturn_DefinitiveTime(EventTimeFrame eventTimeFrame, EventTimeFrameType expectedType)
        {
            var timeFrameType = eventTimeFrame.TimeFrameType;

            Assert.Equal(expectedType, timeFrameType);
        }

        #endregion

        #region Single EventDate Tests

        [Fact]
        public void EventTimeFrame_IsValidTimeFrame_Given_SingleEventDate_EventDateTypeDefinitive_ShouldReturn_True()
        {
            var eventTimeFrame = new EventTimeFrame(new EventDate(EventDateType.Definitive, 0, 0));

            var isValid = eventTimeFrame.IsValidTimeFrame(out _);

            Assert.True(isValid);
        }

        [Fact]
        public void EventTimeFrame_IsValidTimeFrame_Given_SingleEventDate_EventDateTypeDefinitive_ShouldPutOut_EmptyString()
        {
            var eventTimeFrame = new EventTimeFrame(new EventDate(EventDateType.Definitive, 0, 0));

            eventTimeFrame.IsValidTimeFrame(out var invalidFormattingNotes);

            Assert.Empty(invalidFormattingNotes);
        }

        [Theory]
        [InlineData(EventDateType.DefinitiveStart)]
        [InlineData(EventDateType.DefinitiveEnd)]
        [InlineData(EventDateType.SpeculativeStart)]
        [InlineData(EventDateType.SpeculativeEnd)]
        public void EventTimeFrame_IsValidTimeFrame_Given_SingleEventDate_EventDateTypeNotDefinitive_ShouldReturn_False(EventDateType eventDateType)
        {
            var eventTimeFrame = new EventTimeFrame(new EventDate(eventDateType, 0, 0));

            var isValid = eventTimeFrame.IsValidTimeFrame(out _);

            Assert.False(isValid);
        }

        [Theory]
        [InlineData(EventDateType.DefinitiveStart)]
        [InlineData(EventDateType.DefinitiveEnd)]
        [InlineData(EventDateType.SpeculativeStart)]
        [InlineData(EventDateType.SpeculativeEnd)]
        public void EventTimeFrame_IsValidTimeFrame_Given_SingleEventDate_EventDateTypeNotDefinitive_ShouldPutOut_EventTimeFrameFormattingRules(EventDateType eventDateType)
        {
            var expected = EventTimeFrameFormatting.GetFormattingRules();

            var eventTimeFrame = new EventTimeFrame(new EventDate(eventDateType, 0, 0));

            eventTimeFrame.IsValidTimeFrame(out var invalidFormattingNotes);

            Assert.Equal(expected, invalidFormattingNotes);
        }

        #endregion

        #region Two EventDate Tests

        [Theory]
        [InlineData(EventDateType.DefinitiveStart, 0, 0, EventDateType.DefinitiveEnd, 0, 1)]
        [InlineData(EventDateType.DefinitiveEnd, 1, 0, EventDateType.DefinitiveStart, 0, 1)]
        public void EventTimeFrame_IsValidTimeFrame_Given_TwoEventDates_DefinitiveStartAndEnd_WithValidDates_ShouldReturn_True
            (EventDateType dateOneType, int dateOneYear, int dateOneSequence, EventDateType dateTwoType, int dateTwoYear, int dateTwoSequence)
        {
            var eventTimeFrame = new EventTimeFrame(
                   new EventDate(dateOneType, dateOneYear, dateOneSequence),
                   new EventDate(dateTwoType, dateTwoYear, dateTwoSequence)
               );

            var isValid = eventTimeFrame.IsValidTimeFrame(out _);

            Assert.True(isValid);
        }

        [Theory]
        [InlineData(EventDateType.DefinitiveStart, 0, 0, EventDateType.DefinitiveEnd, 0, 1)]
        [InlineData(EventDateType.DefinitiveEnd, 1, 0, EventDateType.DefinitiveStart, 0, 1)]
        public void EventTimeFrame_IsValidTimeFrame_Given_TwoEventDates_DefinitiveStartAndEnd_WithValidDates_ShouldPutOut_EmptyString
           (EventDateType dateOneType, int dateOneYear, int dateOneSequence, EventDateType dateTwoType, int dateTwoYear, int dateTwoSequence)
        {
            var eventTimeFrame = new EventTimeFrame(
                   new EventDate(dateOneType, dateOneYear, dateOneSequence),
                   new EventDate(dateTwoType, dateTwoYear, dateTwoSequence)
               );

            eventTimeFrame.IsValidTimeFrame(out var invalidFormattingNotes);

            Assert.Empty(invalidFormattingNotes);
        }

        [Theory]
        [InlineData(EventDateType.DefinitiveStart, 1, 2, EventDateType.DefinitiveEnd, 1, 1)]
        [InlineData(EventDateType.DefinitiveStart, 0, 0, EventDateType.DefinitiveEnd, 0, 0)]
        [InlineData(EventDateType.DefinitiveEnd, 0, 0, EventDateType.DefinitiveStart, 1, 0)]
        public void EventTimeFrame_IsValidTimeFrame_Given_TwoEventDates_DefinitiveStartAndEnd_WithInvalidDates_ShouldPutOut_EventTimeFrameFormattingRules_DefinitiveStartDefinitiveEnd
          (EventDateType dateOneType, int dateOneYear, int dateOneSequence, EventDateType dateTwoType, int dateTwoYear, int dateTwoSequence)
        {
            var expected = EventTimeFrameFormatting.GetFormattingRules(EventTimeFrameType.DefinitiveStartDefinitiveEnd);

            var eventTimeFrame = new EventTimeFrame(
                   new EventDate(dateOneType, dateOneYear, dateOneSequence),
                   new EventDate(dateTwoType, dateTwoYear, dateTwoSequence)
               );

            eventTimeFrame.IsValidTimeFrame(out var invalidFormattingNotes);

            Assert.Equal(expected, invalidFormattingNotes);
        }

        [Theory]
        [InlineData(EventDateType.DefinitiveStart, 1, 2, EventDateType.DefinitiveEnd, 1, 1)]
        [InlineData(EventDateType.DefinitiveEnd, 0, 0, EventDateType.DefinitiveStart, 1, 0)]
        [InlineData(EventDateType.DefinitiveStart, 0, 0, EventDateType.DefinitiveEnd, 0, 0)]
        public void EventTimeFrame_IsValidTimeFrame_Given_TwoEventDates_DefinitiveStartAndEnd_WithInvalidDates_ShouldReturn_False
          (EventDateType dateOneType, int dateOneYear, int dateOneSequence, EventDateType dateTwoType, int dateTwoYear, int dateTwoSequence)
        {
            var eventTimeFrame = new EventTimeFrame(
                   new EventDate(dateOneType, dateOneYear, dateOneSequence),
                   new EventDate(dateTwoType, dateTwoYear, dateTwoSequence)
               );

            var isValid = eventTimeFrame.IsValidTimeFrame(out _);

            Assert.False(isValid);
        }

        [Theory]
        [InlineData(EventDateType.DefinitiveStart, 1, 2, EventDateType.DefinitiveStart, 1, 1)]
        [InlineData(EventDateType.DefinitiveEnd, 0, 0, EventDateType.DefinitiveEnd, 1, 0)]
        [InlineData(EventDateType.Definitive, 0, 0, EventDateType.DefinitiveEnd, 1, 0)]
        [InlineData(EventDateType.SpeculativeStart, 0, 0, EventDateType.SpeculativeEnd, 1, 0)]
        [InlineData(EventDateType.SpeculativeEnd, 2, 0, EventDateType.DefinitiveStart, 1, 0)]
        [InlineData(EventDateType.Definitive, 0, 0, EventDateType.Definitive, 0, 0)]
        public void EventTimeFrame_IsValidTimeFrame_Given_TwoEventDates_NotDefinitiveStartAndEnd_ShouldReturn_False
          (EventDateType dateOneType, int dateOneYear, int dateOneSequence, EventDateType dateTwoType, int dateTwoYear, int dateTwoSequence)
        {
            var eventTimeFrame = new EventTimeFrame(
                   new EventDate(dateOneType, dateOneYear, dateOneSequence),
                   new EventDate(dateTwoType, dateTwoYear, dateTwoSequence)
               );

            var isValid = eventTimeFrame.IsValidTimeFrame(out _);

            Assert.False(isValid);
        }

        [Theory]
        [InlineData(EventDateType.DefinitiveStart, 1, 2, EventDateType.DefinitiveStart, 1, 1)]
        [InlineData(EventDateType.DefinitiveEnd, 0, 0, EventDateType.DefinitiveEnd, 1, 0)]
        [InlineData(EventDateType.Definitive, 0, 0, EventDateType.DefinitiveEnd, 1, 0)]
        [InlineData(EventDateType.SpeculativeStart, 0, 0, EventDateType.SpeculativeEnd, 1, 0)]
        [InlineData(EventDateType.SpeculativeEnd, 2, 0, EventDateType.DefinitiveStart, 1, 0)]
        [InlineData(EventDateType.Definitive, 0, 0, EventDateType.Definitive, 0, 0)]
        public void EventTimeFrame_IsValidTimeFrame_Given_TwoEventDates_NotDefinitiveStartAndEnd_ShouldPutOut_EventTimeFrameFormattingRules
          (EventDateType dateOneType, int dateOneYear, int dateOneSequence, EventDateType dateTwoType, int dateTwoYear, int dateTwoSequence)
        {
            var expected = EventTimeFrameFormatting.GetFormattingRules();

            var eventTimeFrame = new EventTimeFrame(
                   new EventDate(dateOneType, dateOneYear, dateOneSequence),
                   new EventDate(dateTwoType, dateTwoYear, dateTwoSequence)
               );

            eventTimeFrame.IsValidTimeFrame(out var invalidFormattingNotes);

            Assert.Equal(expected, invalidFormattingNotes);
        }

        #endregion

        #region Three EventDate Tests

        [Theory]
        [InlineData(EventDateType.DefinitiveStart, 0, 0, EventDateType.SpeculativeEnd, 1, 1, EventDateType.SpeculativeEnd, 3, 1)]
        [InlineData(EventDateType.SpeculativeEnd, 2, 0, EventDateType.SpeculativeEnd, 1, 0, EventDateType.DefinitiveStart, 0, 0)]
        [InlineData(EventDateType.DefinitiveEnd, 2, 2, EventDateType.SpeculativeStart, 1, 10, EventDateType.SpeculativeStart, 1, 8)]
        [InlineData(EventDateType.SpeculativeStart, 15, 1, EventDateType.SpeculativeStart, 15, 10, EventDateType.DefinitiveEnd, 16, 1)]
        public void EventTimeFrame_IsValidTimeFrame_Given_ThreeEventDates_WithValidDates_ShouldReturn_True
            (EventDateType dateOneType, int dateOneYear, int dateOneSequence, EventDateType dateTwoType, int dateTwoYear, int dateTwoSequence,
            EventDateType dateThreeType, int dateThreeYear, int dateThreeSequence)
        {
            var eventTimeFrame = new EventTimeFrame(
                   new EventDate(dateOneType, dateOneYear, dateOneSequence),
                   new EventDate(dateTwoType, dateTwoYear, dateTwoSequence),
                   new EventDate(dateThreeType, dateThreeYear, dateThreeSequence)
               );

            var isValid = eventTimeFrame.IsValidTimeFrame(out _);

            Assert.True(isValid);
        }

        [Theory]
        [InlineData(EventDateType.DefinitiveStart, 0, 0, EventDateType.SpeculativeEnd, 1, 1, EventDateType.SpeculativeEnd, 3, 1)]
        [InlineData(EventDateType.SpeculativeEnd, 2, 0, EventDateType.SpeculativeEnd, 1, 0, EventDateType.DefinitiveStart, 0, 0)]
        [InlineData(EventDateType.DefinitiveEnd, 2, 2, EventDateType.SpeculativeStart, 1, 10, EventDateType.SpeculativeStart, 1, 8)]
        [InlineData(EventDateType.SpeculativeStart, 15, 1, EventDateType.SpeculativeStart, 15, 10, EventDateType.DefinitiveEnd, 16, 1)]
        public void EventTimeFrame_IsValidTimeFrame_Given_ThreeEventDates_WithValidDates_ShouldPutOut_EmptyString
            (EventDateType dateOneType, int dateOneYear, int dateOneSequence, EventDateType dateTwoType, int dateTwoYear, int dateTwoSequence,
            EventDateType dateThreeType, int dateThreeYear, int dateThreeSequence)
        {
            var eventTimeFrame = new EventTimeFrame(
                   new EventDate(dateOneType, dateOneYear, dateOneSequence),
                   new EventDate(dateTwoType, dateTwoYear, dateTwoSequence),
                   new EventDate(dateThreeType, dateThreeYear, dateThreeSequence)
               );

            eventTimeFrame.IsValidTimeFrame(out var invalidFormattingNotes);

            Assert.Empty(invalidFormattingNotes);
        }

        [Theory]
        [InlineData(EventDateType.Definitive, 0, 0, EventDateType.DefinitiveStart, 0, 0, EventDateType.DefinitiveEnd, 0, 0)]
        [InlineData(EventDateType.DefinitiveStart, 0, 0, EventDateType.SpeculativeEnd, 1, 0, EventDateType.SpeculativeEnd, 1, 0)]
        [InlineData(EventDateType.DefinitiveStart, 1, 5, EventDateType.SpeculativeEnd, 1, 2, EventDateType.SpeculativeEnd, 1, 3)]
        [InlineData(EventDateType.DefinitiveEnd, 1, 5, EventDateType.SpeculativeEnd, 2, 1, EventDateType.SpeculativeEnd, 2, 3)]
        [InlineData(EventDateType.DefinitiveEnd, 1, 5, EventDateType.SpeculativeStart, 2, 1, EventDateType.SpeculativeEnd, 2, 3)]
        public void EventTimeFrame_IsValidTimeFrame_Given_ThreeEventDates_WithInvalidDates_ShouldReturn_False
            (EventDateType dateOneType, int dateOneYear, int dateOneSequence, EventDateType dateTwoType, int dateTwoYear, int dateTwoSequence,
            EventDateType dateThreeType, int dateThreeYear, int dateThreeSequence)
        {
            var eventTimeFrame = new EventTimeFrame(
                   new EventDate(dateOneType, dateOneYear, dateOneSequence),
                   new EventDate(dateTwoType, dateTwoYear, dateTwoSequence),
                   new EventDate(dateThreeType, dateThreeYear, dateThreeSequence)
               );

            var isValid = eventTimeFrame.IsValidTimeFrame(out _);

            Assert.False(isValid);
        }

        [Theory]
        [InlineData(EventDateType.Definitive, 0, 0, EventDateType.DefinitiveStart, 0, 0, EventDateType.DefinitiveEnd, 0, 0, EventTimeFrameType.Invalid)]
        [InlineData(EventDateType.DefinitiveEnd, 1, 5, EventDateType.SpeculativeEnd, 2, 1, EventDateType.SpeculativeEnd, 2, 3, EventTimeFrameType.Invalid)]
        [InlineData(EventDateType.DefinitiveStart, 0, 0, EventDateType.SpeculativeEnd, 1, 0, EventDateType.SpeculativeEnd, 1, 0, EventTimeFrameType.DefinitiveStartSpeculativeEnd)]
        [InlineData(EventDateType.DefinitiveStart, 1, 5, EventDateType.SpeculativeEnd, 1, 2, EventDateType.SpeculativeEnd, 1, 3, EventTimeFrameType.DefinitiveStartSpeculativeEnd)]
        [InlineData(EventDateType.DefinitiveEnd, 1, 5, EventDateType.SpeculativeStart, 2, 1, EventDateType.SpeculativeStart, 2, 3, EventTimeFrameType.SpeculativeStartDefinitiveEnd)]
        public void EventTimeFrame_IsValidTimeFrame_Given_ThreeEventDates_WithInvalidDates_ShouldPutOut_ExpectedFormattingRules
            (EventDateType dateOneType, int dateOneYear, int dateOneSequence, EventDateType dateTwoType, int dateTwoYear, int dateTwoSequence,
            EventDateType dateThreeType, int dateThreeYear, int dateThreeSequence, EventTimeFrameType expectedFormattingRules)
        {
            var expected = EventTimeFrameFormatting.GetFormattingRules(expectedFormattingRules);

            var eventTimeFrame = new EventTimeFrame(
                   new EventDate(dateOneType, dateOneYear, dateOneSequence),
                   new EventDate(dateTwoType, dateTwoYear, dateTwoSequence),
                   new EventDate(dateThreeType, dateThreeYear, dateThreeSequence)
               );

            eventTimeFrame.IsValidTimeFrame(out var invalidFormattingNotes);

            Assert.Equal(expected, invalidFormattingNotes);
        }

        #endregion

        #region Four EventDate Tests

        [Theory]
        [InlineData(EventDateType.SpeculativeStart, 1, 1, EventDateType.SpeculativeStart, 2, 1, EventDateType.SpeculativeEnd, 5, 1, EventDateType.SpeculativeEnd, 5, 10)]
        [InlineData(EventDateType.SpeculativeEnd, 1, 1, EventDateType.SpeculativeEnd, 2, 1, EventDateType.SpeculativeStart, 0, 1, EventDateType.SpeculativeStart, -1, 10)]
        public void EventTimeFrame_IsValidTimeFrame_Given_FourEventDates_WithValidDates_ShouldReturn_True
            (EventDateType dateOneType, int dateOneYear, int dateOneSequence, EventDateType dateTwoType, int dateTwoYear, int dateTwoSequence,
            EventDateType dateThreeType, int dateThreeYear, int dateThreeSequence, EventDateType dateFourType, int dateFourYear, int dateFourSequence)
        {
            var eventTimeFrame = new EventTimeFrame(
                   new EventDate(dateOneType, dateOneYear, dateOneSequence),
                   new EventDate(dateTwoType, dateTwoYear, dateTwoSequence),
                   new EventDate(dateThreeType, dateThreeYear, dateThreeSequence),
                   new EventDate(dateFourType, dateFourYear, dateFourSequence)
               );

            var isValid = eventTimeFrame.IsValidTimeFrame(out _);

            Assert.True(isValid);
        }

        [Theory]
        [InlineData(EventDateType.SpeculativeStart, 1, 1, EventDateType.SpeculativeStart, 1, 1, EventDateType.SpeculativeEnd, 5, 1, EventDateType.SpeculativeEnd, 5, 1)]
        [InlineData(EventDateType.SpeculativeStart, 1, 1, EventDateType.SpeculativeStart, 1, 1, EventDateType.SpeculativeEnd, 5, 1, EventDateType.SpeculativeEnd, 5, 10)]
        [InlineData(EventDateType.SpeculativeStart, 1, 1, EventDateType.SpeculativeStart, 2, 1, EventDateType.SpeculativeEnd, 5, 1, EventDateType.SpeculativeEnd, 5, 1)]
        [InlineData(EventDateType.SpeculativeEnd, 10, 1, EventDateType.SpeculativeEnd, 12, 1, EventDateType.DefinitiveStart, 5, 1, EventDateType.Definitive, 5, 1)]
        [InlineData(EventDateType.SpeculativeEnd, 10, 1, EventDateType.SpeculativeEnd, 12, 1, EventDateType.SpeculativeStart, 5, 1, EventDateType.DefinitiveStart, 5, 1)]
        [InlineData(EventDateType.SpeculativeStart, 10, 1, EventDateType.SpeculativeStart, 12, 1, EventDateType.SpeculativeEnd, 5, 1, EventDateType.SpeculativeEnd, 5, 10)]
        public void EventTimeFrame_IsValidTimeFrame_Given_FourEventDates_WithInvalidDates_ShouldReturn_False
            (EventDateType dateOneType, int dateOneYear, int dateOneSequence, EventDateType dateTwoType, int dateTwoYear, int dateTwoSequence,
            EventDateType dateThreeType, int dateThreeYear, int dateThreeSequence, EventDateType dateFourType, int dateFourYear, int dateFourSequence)
        {
            var eventTimeFrame = new EventTimeFrame(
                   new EventDate(dateOneType, dateOneYear, dateOneSequence),
                   new EventDate(dateTwoType, dateTwoYear, dateTwoSequence),
                   new EventDate(dateThreeType, dateThreeYear, dateThreeSequence),
                   new EventDate(dateFourType, dateFourYear, dateFourSequence)
               );

            var isValid = eventTimeFrame.IsValidTimeFrame(out _);

            Assert.False(isValid);
        }

        [Theory]
        [InlineData(EventDateType.SpeculativeStart, 1, 1, EventDateType.SpeculativeStart, 1, 1, EventDateType.SpeculativeEnd, 5, 1, EventDateType.SpeculativeEnd, 5, 1, EventTimeFrameType.SpeculativeStartSpeculativeEnd)]
        [InlineData(EventDateType.SpeculativeStart, 1, 1, EventDateType.SpeculativeStart, 1, 1, EventDateType.SpeculativeEnd, 5, 1, EventDateType.SpeculativeEnd, 5, 10, EventTimeFrameType.SpeculativeStartSpeculativeEnd)]
        [InlineData(EventDateType.SpeculativeStart, 1, 1, EventDateType.SpeculativeStart, 2, 1, EventDateType.SpeculativeEnd, 5, 1, EventDateType.SpeculativeEnd, 5, 1, EventTimeFrameType.SpeculativeStartSpeculativeEnd)]
        [InlineData(EventDateType.SpeculativeStart, 10, 1, EventDateType.SpeculativeStart, 12, 1, EventDateType.SpeculativeEnd, 5, 1, EventDateType.SpeculativeEnd, 5, 10, EventTimeFrameType.SpeculativeStartSpeculativeEnd)]
        [InlineData(EventDateType.SpeculativeEnd, 10, 1, EventDateType.SpeculativeEnd, 12, 1, EventDateType.DefinitiveStart, 5, 1, EventDateType.Definitive, 5, 1, EventTimeFrameType.Invalid)]
        [InlineData(EventDateType.SpeculativeEnd, 10, 1, EventDateType.SpeculativeEnd, 12, 1, EventDateType.SpeculativeStart, 5, 1, EventDateType.DefinitiveStart, 5, 1, EventTimeFrameType.Invalid)]
        public void EventTimeFrame_IsValidTimeFrame_Given_FourEventDates_WithInvalidDates_ShouldPutOut_ExpectedFormattingRules
            (EventDateType dateOneType, int dateOneYear, int dateOneSequence, EventDateType dateTwoType, int dateTwoYear, int dateTwoSequence,
            EventDateType dateThreeType, int dateThreeYear, int dateThreeSequence, EventDateType dateFourType, int dateFourYear, int dateFourSequence, EventTimeFrameType expectedFormattingRules)
        {
            var expected = EventTimeFrameFormatting.GetFormattingRules(expectedFormattingRules);

            var eventTimeFrame = new EventTimeFrame(
                    new EventDate(dateOneType, dateOneYear, dateOneSequence),
                    new EventDate(dateTwoType, dateTwoYear, dateTwoSequence),
                    new EventDate(dateThreeType, dateThreeYear, dateThreeSequence),
                    new EventDate(dateFourType, dateFourYear, dateFourSequence)
                );

            eventTimeFrame.IsValidTimeFrame(out var invalidFormattingNotes);

            Assert.Equal(expected, invalidFormattingNotes);
        }

        #endregion

        #region Too Many EventDate Tests

        [Fact]
        public void EventTimeFrame_IsValidTimeFrame_Given_MoreThanFourEventDates_ShouldReturn_False()
        {
            var eventTimeFrame = new EventTimeFrame(
                    new EventDate(EventDateType.SpeculativeStart, 0, 1),
                    new EventDate(EventDateType.SpeculativeStart, 1, 1),
                    new EventDate(EventDateType.SpeculativeStart, 2, 1),
                    new EventDate(EventDateType.SpeculativeEnd, 4, 1),
                    new EventDate(EventDateType.SpeculativeEnd, 5, 1)
                );

            var isValid = eventTimeFrame.IsValidTimeFrame(out _);

            Assert.False(isValid);
        }

        [Fact]
        public void EventTimeFrame_IsValidTimeFrame_Given_MoreThanFourEventDates_ShouldPutOut_EventTimeFrameFormattingRules()
        {
            var expected = EventTimeFrameFormatting.GetFormattingRules();

            var eventTimeFrame = new EventTimeFrame(
                    new EventDate(EventDateType.SpeculativeStart, 0, 1),
                    new EventDate(EventDateType.SpeculativeStart, 1, 1),
                    new EventDate(EventDateType.SpeculativeStart, 2, 1),
                    new EventDate(EventDateType.SpeculativeEnd, 4, 1),
                    new EventDate(EventDateType.SpeculativeEnd, 5, 1)
                );

            eventTimeFrame.IsValidTimeFrame(out var invalidFormattingNotes);

            Assert.Equal(expected, invalidFormattingNotes);
        }

        #endregion
    }
}
