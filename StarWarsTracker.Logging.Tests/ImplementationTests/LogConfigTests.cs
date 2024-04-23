using StarWarsTracker.Domain.Constants.LogConfigs;
using StarWarsTracker.Domain.Enums;
using StarWarsTracker.Logging.AppSettingsConfig;
using StarWarsTracker.Logging.Implementation;

namespace StarWarsTracker.Logging.Tests.ImplementationTests
{
    public class LogConfigTests
    {
        #region Private Members

        private readonly Dictionary<string, LogConfigSettings> _configs;

        private const string _endpointOverrideKey = "ExampleEndpoint";

        private const string _defaultConfigsKey = Category.Default;

        private readonly LogConfig _logConfig;

        #endregion

        #region Constructor 

        public LogConfigTests()
        {            
            _configs = new()
            {
                {
                    _defaultConfigsKey,
                    new()
                    {
                        {
                            "CategoryOne",
                            new()
                            {
                                {
                                    "SectionOne",
                                    new()
                                    {
                                        { "Key1", LogLevel.Trace.ToString() },
                                        { "Key2", LogLevel.Debug.ToString() },
                                        { "Key3", LogLevel.Information.ToString() },
                                        { "Key4", LogLevel.Warning.ToString() },
                                        { "Key5", LogLevel.Error.ToString() },
                                        { "Key6", LogLevel.Critical.ToString() },
                                    }
                                }
                            }
                        }
                    }
                },
                {
                    _endpointOverrideKey,
                    new()
                    {
                        {
                            "CategoryOne",
                            new()
                            {
                                {
                                    "SectionOne",
                                    new()
                                    {
                                        { "Key1", LogLevel.None.ToString() },
                                        { "Key2", LogLevel.None.ToString() },
                                        { "Key3", LogLevel.None.ToString() },
                                        { "Key4", LogLevel.None.ToString() },
                                        { "Key5", LogLevel.None.ToString() },
                                        { "Key6", LogLevel.None.ToString() },
                                    }
                                }
                            }
                        }
                    }
                }
            };

            _logConfig = new(_configs);

        }

        #endregion

        #region Constructor Test (KeyNotFoundException When Default Configs Missing)
        
        [Fact]
        public void LogConfig_Given_DefaultConfigsNotExisting_ShouldThrow_KeyNotFoundException()
        {
            _configs.Remove(_defaultConfigsKey);

            var exception = Record.Exception(() => new LogConfig(_configs));

            Assert.IsType<KeyNotFoundException>(exception);
        }

        #endregion

        #region GetDefaultConfigs Test

        [Fact]
        public void GetDefaultConfigs_Given_DefaultConfigsExist_ShouldReturn_DefaultConfigs()
        {
            var expected = _configs[_defaultConfigsKey]
                .ToDictionary(_ => _.Key, _ => _.Value
                    .ToDictionary(_ => _.Key, _ => _.Value
                        .ToDictionary(_ => _.Key, _ => Enum.Parse<LogLevel>(_.Value))));

            var results = _logConfig.GetDefaultConfigs();

            Assert.Equal(expected, results);
        }

        #endregion

        #region GetEndpointConfigs Tests

        [Fact]
        public void GetEndpointConfigs_Given_EndpointConfigsExist_ShouldReturn_EndpointConfigs()
        {
            var expected = _configs[_endpointOverrideKey]
                .ToDictionary(_ => _.Key, _ => _.Value
                    .ToDictionary(_ => _.Key, _ => _.Value
                        .ToDictionary(_ => _.Key, _ => Enum.Parse<LogLevel>(_.Value))));

            var results = _logConfig.GetEndpointConfigs(_endpointOverrideKey);

            Assert.Equal(expected, results);
        }

        [Fact]
        public void GetEndpointConfigs_Given_EndpointConfigsNotExisting_ShouldReturn_Null()
        {
            var results = _logConfig.GetEndpointConfigs("FakeKey");

            Assert.Null(results);
        }

        [Theory]
        [InlineData("")]
        [InlineData("  ")]
        [InlineData(null)]
        public void GetEndpointConfigs_Given_NullEmptyOrWhitespace_ShouldReturn_Null(string input)
        {
            var results = _logConfig.GetEndpointConfigs(input);

            Assert.Null(results);
        }

        #endregion
    }
}
