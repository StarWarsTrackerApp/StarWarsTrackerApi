using StarWarsTracker.Domain.Enums;
using StarWarsTracker.Logging.AppSettingsConfig;

namespace StarWarsTracker.Logging.Tests.AppSettingsConfigTests
{
    public class LogConfigSectionTests
    {
        [Fact]
        public void ToLogLevelDictionary_Given_AllValuesAreValidLogLevels_ShouldReturn_DictionaryOfLogLevels()
        {
            var logConfigSection = new LogConfigSection()
            {
                { "Key1", LogLevel.Trace.ToString() },
                { "Key2", LogLevel.Debug.ToString() },
                { "Key3", LogLevel.Information.ToString() },
                { "Key4", LogLevel.Warning.ToString() },
                { "Key5", LogLevel.Error.ToString() },
                { "Key6", LogLevel.Critical.ToString() },
                { "Key7", LogLevel.Trace.ToString().ToUpper() },
                { "Key8", LogLevel.Trace.ToString().ToLower() },
            };

            var expected = logConfigSection.ToDictionary(_ => _.Key, _ => Enum.Parse<LogLevel>(_.Value, ignoreCase: true));

            var results = logConfigSection.ToLogLevelDictionary();

            Assert.Equal(expected, results);
        }

        [Fact]
        public void ToLogLevelDictionary_Given_InvalidLogLevelValue_ShouldThrow_ApplicationException()
        {
            (string Key, string Value) badLogLevelConfig = ("Key", "Not a real LogLevel");

            var logConfigSection = new LogConfigSection()
            {
                { "GoodKey", LogLevel.Trace.ToString() },
                { badLogLevelConfig.Key, badLogLevelConfig.Value }
            };

            var exception = Record.Exception(() => logConfigSection.ToLogLevelDictionary());

            Assert.IsType<ApplicationException>(exception);
            Assert.Contains(badLogLevelConfig.Key, exception.Message);
            Assert.Contains(badLogLevelConfig.Value, exception.Message);
        }
    }
}
