using StarWarsTracker.Domain.Models;

namespace StarWarsTracker.Domain.Tests.ModelTests
{
    public class EventDateTests
    {
        [Theory]
        [InlineData(0, 0, 0, 1, true)]
        [InlineData(0, 5, 1, 0, true)]
        [InlineData(0, 5, 2, 0, true)]
        [InlineData(1, 0, 2, 0, true)]
        [InlineData(1, 5, 2, 0, true)]
        [InlineData(0, 0, 0, 0, false)]
        [InlineData(0, 5, 0, 5, false)]
        [InlineData(1, 5, 1, 5, false)]
        [InlineData(1, 0, 1, 0, false)]
        [InlineData(0, 1, 0, 0, false)]
        [InlineData(1, 5, 1, 0, false)]
        [InlineData(1, 1, 0, 0, false)]
        [InlineData(2, 0, 1, 5, false)]
        public void EventDate_OperatorOverload_IsLessThan_Given_EventDates_ShouldReturn_ExpectedOutput(int yearOne, int sequenceOne, int yearTwo, int sequenceTwo, bool expected)
        {
            var eventDateOne = new EventDate()
            {
                YearsSinceBattleOfYavin = yearOne,
                Sequence = sequenceOne
            };

            var eventDateTwo = new EventDate()
            {
                YearsSinceBattleOfYavin = yearTwo,
                Sequence = sequenceTwo
            };

            var result = eventDateOne < eventDateTwo;

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(0, 1, 0, 0, true)]
        [InlineData(1, 5, 1, 0, true)]
        [InlineData(2, 0, 1, 5, true)]
        [InlineData(1, 1, 0, 0, true)]
        [InlineData(0, 0, 0, 0, false)]
        [InlineData(0, 5, 0, 5, false)]
        [InlineData(1, 5, 1, 5, false)]
        [InlineData(1, 0, 1, 0, false)]
        [InlineData(0, 0, 0, 1, false)]
        [InlineData(0, 5, 1, 0, false)]
        [InlineData(0, 5, 2, 0, false)]
        [InlineData(1, 0, 2, 0, false)]
        [InlineData(1, 5, 2, 0, false)]
        public void EventDate_OperatorOverload_IsGreaterThan_Given_EventDates_ShouldReturn_ExpectedOutput(int yearOne, int sequenceOne, int yearTwo, int sequenceTwo, bool expected)
        {
            var eventDateOne = new EventDate()
            {
                YearsSinceBattleOfYavin = yearOne,
                Sequence = sequenceOne
            };

            var eventDateTwo = new EventDate()
            {
                YearsSinceBattleOfYavin = yearTwo,
                Sequence = sequenceTwo
            };

            var result = eventDateOne > eventDateTwo;

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(0, 0, 0, 0, true)]
        [InlineData(0, 5, 0, 5, true)]
        [InlineData(1, 0, 1, 0, true)]
        [InlineData(1, 5, 1, 5, true)]
        [InlineData(0, 1, 0, 0, false)]
        [InlineData(1, 5, 1, 0, false)]
        [InlineData(2, 0, 1, 5, false)]
        [InlineData(1, 1, 0, 0, false)]
        [InlineData(0, 0, 0, 1, false)]
        [InlineData(0, 5, 1, 0, false)]
        [InlineData(0, 5, 2, 0, false)]
        [InlineData(1, 0, 2, 0, false)]
        [InlineData(1, 5, 2, 0, false)]
        public void EventDate_OperatorOverload_IsEqualTo_Given_EventDates_ShouldReturn_ExpectedOutput(int yearOne, int sequenceOne, int yearTwo, int sequenceTwo, bool expected)
        {
            var eventDateOne = new EventDate()
            {
                YearsSinceBattleOfYavin = yearOne,
                Sequence = sequenceOne
            };

            var eventDateTwo = new EventDate()
            {
                YearsSinceBattleOfYavin = yearTwo,
                Sequence = sequenceTwo
            };

            var result = eventDateOne == eventDateTwo;

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(0, 1, 0, 0, true)]
        [InlineData(1, 5, 1, 0, true)]
        [InlineData(2, 0, 1, 5, true)]
        [InlineData(1, 1, 0, 0, true)]
        [InlineData(0, 0, 0, 1, true)]
        [InlineData(0, 5, 1, 0, true)]
        [InlineData(0, 5, 2, 0, true)]
        [InlineData(1, 0, 2, 0, true)]
        [InlineData(1, 5, 2, 0, true)]
        [InlineData(0, 0, 0, 0, false)]
        [InlineData(0, 5, 0, 5, false)]
        [InlineData(1, 0, 1, 0, false)]
        [InlineData(1, 5, 1, 5, false)]
        public void EventDate_OperatorOverload_IsNotEqualTo_Given_EventDates_ShouldReturn_ExpectedOutput(int yearOne, int sequenceOne, int yearTwo, int sequenceTwo, bool expected)
        {
            var eventDateOne = new EventDate()
            {
                YearsSinceBattleOfYavin = yearOne,
                Sequence = sequenceOne
            };

            var eventDateTwo = new EventDate()
            {
                YearsSinceBattleOfYavin = yearTwo,
                Sequence = sequenceTwo
            };

            var result = eventDateOne != eventDateTwo;

            Assert.Equal(expected, result);
        }
    }
}
