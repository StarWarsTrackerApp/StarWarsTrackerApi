using StarWarsTracker.Domain.Constants;
using StarWarsTracker.Domain.Enums;
using StarWarsTracker.Domain.Models;

namespace StarWarsTracker.Domain.Tests.ModelTests
{
    public class EventTimeFrameTests
    {
        #region Single EventDate Tests

        [Fact]
        public void EventTimeFrame_IsValidTimeFrame_Given_SingleEventDate_EventDateTypeDefinitive_ShouldReturn_True()
        {
            var eventTimeFrame = new EventTimeFrame(new EventDate(EventDateType.Definitive, 0, 0));

            var isValid = eventTimeFrame.IsValidTimeFrame(out _);

            Assert.True(isValid);
        }

        [Fact]
        public void EventTimeFrame_IsValidTimeFrame_Given_SingleEventDate_EventDateTypeDefinitive_ShouldPutOut_EmptyInvalidFormattingNotes()
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
            var eventDate = new EventDate(eventDateType, 0, 0);

            var eventTimeFrame = new EventTimeFrame(eventDate);

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
            (EventDateType eventOneDateType, int eventOneYear, int eventOneSequence, EventDateType eventTwoDateType, int eventTwoYear, int eventTwoSequence)
        {
            var eventOne = new EventDate(eventOneDateType, eventOneYear, eventOneSequence);

            var eventTwo = new EventDate(eventTwoDateType, eventTwoYear, eventTwoSequence);

            var eventTimeFrame = new EventTimeFrame(eventOne, eventTwo);

            var isValid = eventTimeFrame.IsValidTimeFrame(out _);

            Assert.True(isValid);
        }

        [Theory]
        [InlineData(EventDateType.DefinitiveStart, 0, 0, EventDateType.DefinitiveEnd, 0, 1)]
        [InlineData(EventDateType.DefinitiveEnd, 1, 0, EventDateType.DefinitiveStart, 0, 1)]
        public void EventTimeFrame_IsValidTimeFrame_Given_TwoEventDates_DefinitiveStartAndEnd_WithValidDates_ShouldPutOut_EmptyInvalidFormattingNotes
           (EventDateType eventOneDateType, int eventOneYear, int eventOneSequence, EventDateType eventTwoDateType, int eventTwoYear, int eventTwoSequence)
        {
            var eventOne = new EventDate(eventOneDateType, eventOneYear, eventOneSequence);

            var eventTwo = new EventDate(eventTwoDateType, eventTwoYear, eventTwoSequence);

            var eventTimeFrame = new EventTimeFrame(eventOne, eventTwo);

            eventTimeFrame.IsValidTimeFrame(out var invalidFormattingNotes);

            Assert.Empty(invalidFormattingNotes);
        }

        [Theory]
        [InlineData(EventDateType.DefinitiveStart, 1, 2, EventDateType.DefinitiveEnd, 1, 1)]
        [InlineData(EventDateType.DefinitiveEnd, 0, 0, EventDateType.DefinitiveStart, 1, 0)]
        public void EventTimeFrame_IsValidTimeFrame_Given_TwoEventDates_DefinitiveStartAndEnd_WithInvalidDates_ShouldPutOut_EventTimeFrameFormattingRules_DefinitiveStartDefinitiveEnd
          (EventDateType eventOneDateType, int eventOneYear, int eventOneSequence, EventDateType eventTwoDateType, int eventTwoYear, int eventTwoSequence)
        {
            var expected = EventTimeFrameFormatting.GetFormattingRules(EventTimeFrameType.DefinitiveStartDefinitiveEnd);

            var eventOne = new EventDate(eventOneDateType, eventOneYear, eventOneSequence);

            var eventTwo = new EventDate(eventTwoDateType, eventTwoYear, eventTwoSequence);

            var eventTimeFrame = new EventTimeFrame(eventOne, eventTwo);

            eventTimeFrame.IsValidTimeFrame(out var invalidFormattingNotes);

            Assert.Equal(expected, invalidFormattingNotes);
        }

        [Theory]
        [InlineData(EventDateType.DefinitiveStart, 1, 2, EventDateType.DefinitiveEnd, 1, 1)]
        [InlineData(EventDateType.DefinitiveEnd, 0, 0, EventDateType.DefinitiveStart, 1, 0)]
        public void EventTimeFrame_IsValidTimeFrame_Given_TwoEventDates_DefinitiveStartAndEnd_WithInvalidDates_ShouldReturn_False
          (EventDateType eventOneDateType, int eventOneYear, int eventOneSequence, EventDateType eventTwoDateType, int eventTwoYear, int eventTwoSequence)
        {
            var eventOne = new EventDate(eventOneDateType, eventOneYear, eventOneSequence);

            var eventTwo = new EventDate(eventTwoDateType, eventTwoYear, eventTwoSequence);

            var eventTimeFrame = new EventTimeFrame(eventOne, eventTwo);

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
          (EventDateType eventOneDateType, int eventOneYear, int eventOneSequence, EventDateType eventTwoDateType, int eventTwoYear, int eventTwoSequence)
        {
            var eventOne = new EventDate(eventOneDateType, eventOneYear, eventOneSequence);

            var eventTwo = new EventDate(eventTwoDateType, eventTwoYear, eventTwoSequence);

            var eventTimeFrame = new EventTimeFrame(eventOne, eventTwo);

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
          (EventDateType eventOneDateType, int eventOneYear, int eventOneSequence, EventDateType eventTwoDateType, int eventTwoYear, int eventTwoSequence)
        {
            var expected = EventTimeFrameFormatting.GetFormattingRules();

            var eventOne = new EventDate(eventOneDateType, eventOneYear, eventOneSequence);

            var eventTwo = new EventDate(eventTwoDateType, eventTwoYear, eventTwoSequence);

            var eventTimeFrame = new EventTimeFrame(eventOne, eventTwo);

            eventTimeFrame.IsValidTimeFrame(out var invalidFormattingNotes);

            Assert.Equal(expected, invalidFormattingNotes);
        }

        #endregion

        #region Three EventDate Tests



        #endregion
    }
}
