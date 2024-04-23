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

        #region AddContent Tests

        [Fact]
        public void AddContent_Given_LogContentLogLevelIsNone_Should_NotAddContent()
        {
            _content.LogLevel = LogLevel.None;

            _logMessage.AddContent(_content);

            var contents = _logMessage.GetAllContent();

            Assert.Empty(contents);
        }

        [Theory]
        [InlineData(LogLevel.Trace)]
        [InlineData(LogLevel.Debug)]
        [InlineData(LogLevel.Information)]
        [InlineData(LogLevel.Warning)]
        [InlineData(LogLevel.Error)]
        [InlineData(LogLevel.Critical)]
        public void AddContent_Given_LogContentLogLevelNotNone_Should_AddLogContent(LogLevel logLevel)
        {
            _content.LogLevel = logLevel;

            _logMessage.AddContent(_content);

            var contents = _logMessage.GetAllContent();

            Assert.Equal(_content, contents.Single());
        }

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

        #region GetContent Tests

        [Fact]
        public void GetContent_Given_LogLevelNone_ShouldReturn_EmptyCollection()
        {
            _logMessage.AddContent(_content);

            var results = _logMessage.GetContent(LogLevel.None);

            Assert.Empty(results);
        }

        [Theory]
        [InlineData(LogLevel.Trace)]
        [InlineData(LogLevel.Debug)]
        [InlineData(LogLevel.Information)]
        [InlineData(LogLevel.Warning)]
        [InlineData(LogLevel.Error)]
        [InlineData(LogLevel.Critical)]
        public void GetContent_Given_LogLevel_ShouldReturn_LogContentAboveOrEqualToLogLevel(LogLevel logLevel)
        {
            _logMessage.AddContent(new(LogLevel.Trace, "TraceClass", "TraceNamespace", "TraceMethod", "TraceDescription", "TraceExtra", 1.23));
            _logMessage.AddContent(new(LogLevel.Debug, "DebugClass", "DebugNamespace", "DebugMethod", "DebugDescription", "DebugExtra", 2.34));
            _logMessage.AddContent(new(LogLevel.Information, "InformationClass", "InformationNamespace", "InformationMethod", "InformationDescription", "InformationExtra", 3.45));
            _logMessage.AddContent(new(LogLevel.Warning, "WarningClass", "WarningNamespace", "WarningMethod", "WarningDescription", "WarningExtra", 4.56));
            _logMessage.AddContent(new(LogLevel.Error, "ErrorClass", "ErrorNamespace", "ErrorMethod", "ErrorDescription", "ErrorExtra", 5.67));
            _logMessage.AddContent(new(LogLevel.Critical, "CriticalClass", "CriticalNamespace", "CriticalMethod", "CriticalDescription", "CriticalExtra", 6.78));

            var results = _logMessage.GetContent(logLevel);

            Assert.True(results.All(_ => _.LogLevel >= logLevel));
        }

        #endregion
    }
}
