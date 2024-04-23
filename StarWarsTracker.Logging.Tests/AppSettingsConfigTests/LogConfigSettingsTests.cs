using StarWarsTracker.Domain.Enums;
using StarWarsTracker.Logging.AppSettingsConfig;

namespace StarWarsTracker.Logging.Tests.AppSettingsConfigTests
{
    public class LogConfigSettingsTests
    {
        [Fact]
        public void GetConfigs_Given_AllValuesAreValidLogLevels_ShouldReturn_DictionaryOfLogConfigCategories() 
        {
            var logConfigSettings = new LogConfigSettings()
            {
                {
                    "CategoryOneKey",
                    new LogConfigCategory()
                    {
                         {
                            "SectionOneKey",
                            new LogConfigSection()
                            {
                                { "Key1", LogLevel.Trace.ToString() },
                                { "Key2", LogLevel.Debug.ToString() },
                                { "Key3", LogLevel.Information.ToString() },
                                { "Key4", LogLevel.Warning.ToString() },
                                { "Key5", LogLevel.Error.ToString() },
                                { "Key6", LogLevel.Critical.ToString() }
                            }
                         },
                        {
                            "SectionTwoKey",
                            new LogConfigSection()
                            {
                                { "Key1", LogLevel.Trace.ToString().ToUpper() },
                                { "Key2", LogLevel.Debug.ToString().ToUpper() },
                                { "Key3", LogLevel.Information.ToString().ToUpper() },
                                { "Key4", LogLevel.Warning.ToString().ToUpper() },
                                { "Key5", LogLevel.Error.ToString().ToUpper() },
                                { "Key6", LogLevel.Critical.ToString().ToUpper() }
                            }
                        }
                    }
                },
                {
                    "CategoryTwoKey",
                    new LogConfigCategory()
                    {
                        {
                            "SectionOneKey",
                            new LogConfigSection()
                            {
                                { "Key1", LogLevel.Trace.ToString().ToLower() },
                                { "Key2", LogLevel.Debug.ToString().ToLower() },
                                { "Key3", LogLevel.Information.ToString().ToLower() },
                                { "Key4", LogLevel.Warning.ToString().ToLower() },
                                { "Key5", LogLevel.Error.ToString().ToLower() },
                                { "Key6", LogLevel.Critical.ToString().ToLower() }
                            }
                         },
                    }
                }
            };

            var expected = logConfigSettings
                .ToDictionary(_ => _.Key, _ => _.Value
                    .ToDictionary(_ => _.Key, _ => _.Value
                        .ToDictionary(_ => _.Key, _ => Enum.Parse<LogLevel>(_.Value, ignoreCase: true))));

            var results = logConfigSettings.GetConfigs();

            Assert.Equal(expected, results);
        }
    }
}
