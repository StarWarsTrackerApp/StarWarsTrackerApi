using Moq;
using StarWarsTracker.Domain.Enums;
using StarWarsTracker.Logging.Abstraction;
using StarWarsTracker.Logging.Implementation;

namespace StarWarsTracker.Logging.Tests.ImplementationTests
{
    public class LogConfigReaderTests
    {
        #region Private Members

        private readonly Mock<ILogConfig> _mockLogConfig = new();

        private readonly Dictionary<string, Dictionary<string, Dictionary<string, LogLevel>>> _defaultConfigs = new();

        private readonly Dictionary<string, Dictionary<string, Dictionary<string, LogLevel>>> _endpointOverrideConfigs = new();

        private const string _endpointOverridesKey = "EndpointOverridesKey";

        private readonly LogConfigReader _logConfigReader;

        #endregion

        #region Constructor

        public LogConfigReaderTests()
        {
            _mockLogConfig.Setup(_ => _.GetDefaultConfigs()).Returns(_defaultConfigs);
            _mockLogConfig.Setup(_ => _.GetEndpointConfigs(_endpointOverridesKey)).Returns(_endpointOverrideConfigs);

            _logConfigReader = new(_mockLogConfig.Object);
        }

        #endregion

        #region GetActiveConfigs Test

        [Fact]
        public void GetActiveConfigs_Given_EndpointConfigsNotSet_ShouldReturn_DefaultConfigs()
        {
            var expected = _mockLogConfig.Object.GetDefaultConfigs();

            var result = _logConfigReader.GetActiveConfigs();

            Assert.Equal(expected, result);
        }

        [Fact]
        public void GetActiveConfigs_Given_EndpointConfigsSet_ShouldReturn_EndpointOverrides()
        {
            var category = "CategoryKey";
            var section = "SectionKey";
            var key = "Key";
            var defaultLogLevel = LogLevel.Trace;
            var endpointOverrideLogLevel = LogLevel.Critical;

            _defaultConfigs.Add(
               category,
               new()
               {
                    {
                        section,
                        new()
                        {
                            { key, defaultLogLevel },
                        }
                    }
               });

            _endpointOverrideConfigs.Add(
                category,
                new()
                {
                    {
                        section,
                        new()
                        {
                            { key, endpointOverrideLogLevel },
                        }
                    }
                });

            _logConfigReader.TrySetEndpointConfigs(_endpointOverridesKey);

            var expected = _endpointOverrideConfigs;

            var configs = _logConfigReader.GetActiveConfigs();

            Assert.Equal(expected, configs);
        }

        #endregion

        #region GetConfigCategory Tests

        [Fact]
        public void GetConfigCategory_Given_NoCategoryFound_ShouldReturn_Null()
        {
            var categoryKey = "FakeKey";

            var result =_logConfigReader.GetConfigCategory(categoryKey);

            Assert.Null(result);
        }

        [Fact]
        public void GetConfigCategory_Given_ConfigCategoryFound_ShouldReturn_Category()
        {
            var categoryKey = "categoryKey";

            var expectedCategory = new Dictionary<string, Dictionary<string, LogLevel>>();

            _defaultConfigs.Add(categoryKey, expectedCategory);

            var result = _logConfigReader.GetConfigCategory(categoryKey);

            Assert.Equal(expectedCategory, result);
        }

        #endregion

        #region GetConfigSection Tests

        [Fact]
        public void GetConfigSection_Given_NoConfigCategoryFound_ShouldReturn_Null()
        {
            var categoryKey = "FakeKey";
            var sectionKey = "FakeSectionKey";

            var result = _logConfigReader.GetConfigSection(categoryKey, sectionKey);

            Assert.Null(result);
        }

        [Fact]
        public void GetConfigSection_Given_NoConfigSectionFound_ShouldReturn_Null()
        {
            var categoryKey = "ExistingCategoryKey";
            var sectionKey = "FakeSectionKey";

            _defaultConfigs.Add(categoryKey, new());

            var result = _logConfigReader.GetConfigSection(categoryKey, sectionKey);

            Assert.Null(result);
        }

        [Fact]
        public void GetConfigSection_Given_ConfigSectionFound_ShouldReturn_ConfigSection()
        {
            var categoryKey = "ExistingCategoryKey";
            var sectionKey = "ExistingSectionKey";

            var expectedSection = new Dictionary<string, LogLevel>();

            _defaultConfigs.Add(
                categoryKey, 
                new()
                {
                    { sectionKey, expectedSection }
                });

            var result = _logConfigReader.GetConfigSection(categoryKey, sectionKey);

            Assert.Equal(expectedSection, result);
        }

        #endregion

        #region GetLogLevel Tests

        [Fact]
        public void GetLogLevel_Given_NoConfigCategoryFound_ShouldReturn_Null()
        {
            var result = _logConfigReader.GetLogLevel("FakeSection", "FakeKey", "FakeCategory");

            Assert.Null(result);
        }

        [Fact]
        public void GetLogLevel_Given_NoConfigSectionFound_ShouldReturn_Null()
        {
            var categoryKey = "ExistingCategoryKey";

            _defaultConfigs.Add(categoryKey, new());

            var result = _logConfigReader.GetLogLevel("FakeSection", "FakeKey", categoryKey);

            Assert.Null(result);
        }

        [Fact]
        public void GetLogLevel_Given_NoKeyFound_ShouldRetun_Null()
        {
            var categoryKey = "ExistingCategoryKey";
            var sectionKey = "ExistingSectionKey";

            _defaultConfigs.Add(categoryKey, new() { { sectionKey, new() } });

            var result = _logConfigReader.GetLogLevel(sectionKey, "FakeKey", categoryKey);

            Assert.Null(result);
        }

        [Fact]
        public void GetLogLevel_Given_KeyFound_ShouldReturn_Value()
        {
            var categoryKey = "ExistingCategoryKey";
            var sectionKey = "ExistingSectionKey";
            var key = "ExistingKey";
            var expectedValue = LogLevel.Critical;

            _defaultConfigs.Add(categoryKey, new() { { sectionKey, new() { { key, expectedValue } } } });

            var result = _logConfigReader.GetLogLevel(sectionKey, key, categoryKey);

            Assert.Equal(expectedValue, result);
        }

        #endregion

        #region TrySetEndpointConfigs Tests

        [Fact]
        public void TrySetEndpointConfigs_Given_NoEndpointOverridesFound_ShouldRetun_False()
        {
            var result = _logConfigReader.TrySetEndpointConfigs("FakeEndpointRoute");

            Assert.False(result);
        }

        [Fact]
        public void TrySetEndpointConfigs_Given_DefaultConfigNotContainingCategory_Should_AddCategory()
        {
            var categoryKey = "CategoryKey";
            var expectedCategory = new Dictionary<string, Dictionary<string, LogLevel>>();

            _endpointOverrideConfigs.Add(categoryKey, expectedCategory);

            var overrideApplied = _logConfigReader.TrySetEndpointConfigs(_endpointOverridesKey);

            var result = _logConfigReader.GetConfigCategory(categoryKey);

            Assert.True(overrideApplied);
            Assert.Equal(expectedCategory, result);
        }

        [Fact]
        public void TrySetEndpointConfigs_Given_DefaultConfigNotContainingSection_Should_AddSection()
        {
            var categoryKey = "CategoryKey";
            var sectionKey = "SectionKey";
            var expectedSection = new Dictionary<string, LogLevel>();

            _defaultConfigs.Add(categoryKey, new());
            _endpointOverrideConfigs.Add(categoryKey, new() { { sectionKey, expectedSection } });

            var overrideApplied = _logConfigReader.TrySetEndpointConfigs(_endpointOverridesKey);

            var result = _logConfigReader.GetConfigSection(categoryKey, sectionKey);

            Assert.True(overrideApplied);
            Assert.Equal(expectedSection, result);
        }

        [Fact]
        public void TrySetEndpointConfigs_Given_DefaultConfigNotContainingKey_Should_AddKeyValue()
        {
            var categoryKey = "CategoryKey";
            var sectionKey = "SectionKey";
            (string Key,  LogLevel Value) expected = ("Key", LogLevel.Critical);

            _defaultConfigs.Add(categoryKey, new() { {  sectionKey, new() } });
            _endpointOverrideConfigs.Add(categoryKey, new() { { sectionKey, new() { { expected.Key, expected.Value } } } });

            var overrideApplied = _logConfigReader.TrySetEndpointConfigs(_endpointOverridesKey);

            var result = _logConfigReader.GetLogLevel(sectionKey, expected.Key, categoryKey);

            Assert.True(overrideApplied);
            Assert.Equal(expected.Value, result);
        }

        #endregion
    }
}
