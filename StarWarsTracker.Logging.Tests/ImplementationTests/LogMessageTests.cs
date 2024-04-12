using StarWarsTracker.Domain.Enums;
using StarWarsTracker.Logging.Implementation;

namespace StarWarsTracker.Logging.Tests.ImplementationTests
{
    public class LogMessageTests
    {
        #region Private Members

        private readonly LogMessage _logMessage = new ();

        private readonly LogContent _content = new (LogLevel.Trace, "ClassName", "Namespace", "Method", "Description", "Extra", 2.00);

        #endregion

        #region GetMessageLogLevel Tests

        [Fact]
        public void GetMessageLogLevel_Given_IncreaseLevelNotCalled_ShouldReturn_Trace()
        {
            var result = _logMessage.GetLevel();

            Assert.Equal(LogLevel.Trace, result);
        }

        [Theory]
        [InlineData(LogLevel.Trace)]
        [InlineData(LogLevel.Debug)]
        [InlineData(LogLevel.Information)]
        [InlineData(LogLevel.Warning)]
        [InlineData(LogLevel.Error)]
        [InlineData(LogLevel.Critical)]
        public void GetMessageLogLevel_Given_IncreaseLevelCalled_ShouldReturn_IncreasedLevel(LogLevel level)
        {
            _content.LogLevel = level;

            _logMessage.IncreaseLevel(_content);

            var result = _logMessage.GetLevel();

            Assert.Equal(level, result);
        }

        #endregion

        #region IncreaseLevel Tests

        [Theory]
        [InlineData(LogLevel.Debug, LogLevel.Trace)]
        [InlineData(LogLevel.Information, LogLevel.Trace)]
        [InlineData(LogLevel.Information, LogLevel.Debug)]
        [InlineData(LogLevel.Warning, LogLevel.Trace)]
        [InlineData(LogLevel.Warning, LogLevel.Debug)]
        [InlineData(LogLevel.Warning, LogLevel.Information)]
        [InlineData(LogLevel.Error, LogLevel.Trace)]
        [InlineData(LogLevel.Error, LogLevel.Debug)]
        [InlineData(LogLevel.Error, LogLevel.Information)]
        [InlineData(LogLevel.Error, LogLevel.Warning)]
        [InlineData(LogLevel.Critical, LogLevel.Trace)]
        [InlineData(LogLevel.Critical, LogLevel.Debug)]
        [InlineData(LogLevel.Critical, LogLevel.Information)]
        [InlineData(LogLevel.Critical, LogLevel.Warning)]
        [InlineData(LogLevel.Critical, LogLevel.Error)]
        public void IncreaseLevel_Given_NewLevelLowerThanCurrentLevel_Should_LeaveLogLevelAsOriginalHigherLevel(LogLevel higherLevel, LogLevel lowerLevel)
        {
            _content.LogLevel = higherLevel; 
            
            // IncreaseLevel with the higher level
            _logMessage.IncreaseLevel(_content);

            _content.LogLevel = lowerLevel;

            //IncreaseLevel with the lower level
            _logMessage.IncreaseLevel(_content);

            var result = _logMessage.GetLevel();

            Assert.Equal(higherLevel, result);
        }

        [Theory]
        [InlineData(LogLevel.Trace)]
        [InlineData(LogLevel.Debug)]
        [InlineData(LogLevel.Information)]
        [InlineData(LogLevel.Warning)]
        [InlineData(LogLevel.Error)]
        [InlineData(LogLevel.Critical)]
        public void IncreaseLevel_Given_LogLevelNone_Should_LeaveLogLevelAsWhatIsIs(LogLevel logLevel)
        {
            _content.LogLevel = logLevel;

            // Increase the LogLevel to what is passed in
            _logMessage.IncreaseLevel(_content);

            _content.LogLevel = LogLevel.None;

            // Attempt to increase LogLevel to None
            _logMessage.IncreaseLevel(_content);

            var result = _logMessage.GetLevel();

            Assert.Equal(logLevel, result);
        }

        #endregion

    }
}
