using Moq;
using StarWarsTracker.Domain.Constants.LogConfigs;
using StarWarsTracker.Domain.Enums;
using StarWarsTracker.Logging.Abstraction;
using StarWarsTracker.Logging.Implementation;

namespace StarWarsTracker.Logging.Tests.ImplementationTests
{
    public class ClassLoggerTests
    {
        #region Private Members

        private readonly Mock<ILogMessage> _mockLogMessage = new();

        private readonly Mock<ILogConfigReader> _mockLogConfigReader = new();

        private readonly ClassLogger _classLogger;

        #endregion

        #region Private Constants for Expected Values

        private const string _className = "ClassName";

        private const string _nameSpaceName = "NameSpaceName";

        private const string _description = "Description";

        private const string _extra = "Extra";

        private const string _methodCalling = "MethodCalling";

        #endregion

        #region Constructor

        public ClassLoggerTests()
        {
            _classLogger = new(_className, _nameSpaceName, _mockLogMessage.Object, _mockLogConfigReader.Object);
        }

        #endregion

        #region Reuseable Test Case Parameters

        public static IEnumerable<object[]> AllLogLevels = new[]
        {
            new object []{ LogLevel.Trace },
            new object []{ LogLevel.Debug },
            new object []{ LogLevel.Information },
            new object []{ LogLevel.Warning },
            new object []{ LogLevel.Error },
            new object []{ LogLevel.Critical },
            new object []{ LogLevel.None }
        };

        #endregion

        #region AddTrace Tests

        [Fact]
        public void AddTrace_Given_NoOverrides_ShouldAdd_LogContentAsTrace()
        {
            var expectedLogLevel = LogLevel.Trace;

            _classLogger.AddTrace(_description, _extra, _methodCalling);

            AssertAddContentCalledWithLogLevel(expectedLogLevel);
        }

        [Theory]
        [MemberData(nameof(AllLogLevels))]
        public void AddTrace_Given_OnlyNamespaceOverride_ShouldAdd_LogContent_WithNamespaceOverride(LogLevel expectedLogLevel)
        {
            var nameSpaceOverrides = GetNamespaceOverrides(LogLevel.Trace, expectedLogLevel);

            _mockLogConfigReader.Setup(_ => _.GetConfigCategory(Category.OverrideLogLevelByNameSpace))
                                .Returns(() => nameSpaceOverrides);

            var classLogger = new ClassLogger(_className, _nameSpaceName, _mockLogMessage.Object, _mockLogConfigReader.Object);

            classLogger.AddTrace(_description, _extra, _methodCalling);

            AssertAddContentCalledWithLogLevel(expectedLogLevel);
        }

        [Theory]
        [MemberData(nameof(AllLogLevels))]
        public void AddTrace_Given_OnlyClassOverride_ShouldAdd_LogContent_WithClassOverride(LogLevel expectedLogLevel)
        {
            var classNameOverrides = GetClassOverrides(LogLevel.Trace, expectedLogLevel);

            _mockLogConfigReader.Setup(_ => _.GetConfigSection(Category.OverrideLogLevelByClassName, _className))
                                .Returns(() => classNameOverrides);

            var classLogger = new ClassLogger(_className, _nameSpaceName, _mockLogMessage.Object, _mockLogConfigReader.Object);

            classLogger.AddTrace(_description, _extra, _methodCalling);

            AssertAddContentCalledWithLogLevel(expectedLogLevel);
        }

        [Theory]
        [MemberData(nameof(AllLogLevels))]
        public void AddTrace_Given_NamespaceAndClassOverride_ShouldAdd_LogContent_WithClassOverride(LogLevel expectedLogLevel)
        {
            var classNameOverrides = GetClassOverrides(LogLevel.Trace, expectedLogLevel);
            var namespaceOverrides = GetNamespaceOverrides(LogLevel.Trace, LogLevel.None);

            _mockLogConfigReader.Setup(_ => _.GetConfigSection(Category.OverrideLogLevelByClassName, _className))
                                .Returns(() => classNameOverrides);

            _mockLogConfigReader.Setup(_ => _.GetConfigCategory(Category.OverrideLogLevelByNameSpace))
                                .Returns(() => namespaceOverrides);

            var classLogger = new ClassLogger(_className, _nameSpaceName, _mockLogMessage.Object, _mockLogConfigReader.Object);

            classLogger.AddTrace(_description, _extra, _methodCalling);

            AssertAddContentCalledWithLogLevel(expectedLogLevel);
        }

        #endregion

        #region AddDebug Tests

        [Fact]
        public void AddDebug_Given_NoOverrides_ShouldAdd_LogContentAsDebug()
        {
            var expectedLogLevel = LogLevel.Debug;

            _classLogger.AddDebug(_description, _extra, _methodCalling);

            AssertAddContentCalledWithLogLevel(expectedLogLevel);
        }

        [Theory]
        [MemberData(nameof(AllLogLevels))]
        public void AddDebug_Given_OnlyNamespaceOverride_ShouldAdd_LogContent_WithNamespaceOverride(LogLevel expectedLogLevel)
        {
            var nameSpaceOverrides = GetNamespaceOverrides(LogLevel.Debug, expectedLogLevel);

            _mockLogConfigReader.Setup(_ => _.GetConfigCategory(Category.OverrideLogLevelByNameSpace))
                                .Returns(() => nameSpaceOverrides);

            var classLogger = new ClassLogger(_className, _nameSpaceName, _mockLogMessage.Object, _mockLogConfigReader.Object);

            classLogger.AddDebug(_description, _extra, _methodCalling);

            AssertAddContentCalledWithLogLevel(expectedLogLevel);
        }

        [Theory]
        [MemberData(nameof(AllLogLevels))]
        public void AddDebug_Given_OnlyClassOverride_ShouldAdd_LogContent_WithClassOverride(LogLevel expectedLogLevel)
        {
            var classNameOverrides = GetClassOverrides(LogLevel.Debug, expectedLogLevel);

            _mockLogConfigReader.Setup(_ => _.GetConfigSection(Category.OverrideLogLevelByClassName, _className))
                                .Returns(() => classNameOverrides);

            var classLogger = new ClassLogger(_className, _nameSpaceName, _mockLogMessage.Object, _mockLogConfigReader.Object);

            classLogger.AddDebug(_description, _extra, _methodCalling);

            AssertAddContentCalledWithLogLevel(expectedLogLevel);
        }

        [Theory]
        [MemberData(nameof(AllLogLevels))]
        public void AddDebug_Given_NamespaceAndClassOverride_ShouldAdd_LogContent_WithClassOverride(LogLevel expectedLogLevel)
        {
            var classNameOverrides = GetClassOverrides(LogLevel.Debug, expectedLogLevel);
            var namespaceOverrides = GetNamespaceOverrides(LogLevel.Debug, LogLevel.None);

            _mockLogConfigReader.Setup(_ => _.GetConfigSection(Category.OverrideLogLevelByClassName, _className))
                                .Returns(() => classNameOverrides);

            _mockLogConfigReader.Setup(_ => _.GetConfigCategory(Category.OverrideLogLevelByNameSpace))
                                .Returns(() => namespaceOverrides);

            var classLogger = new ClassLogger(_className, _nameSpaceName, _mockLogMessage.Object, _mockLogConfigReader.Object);

            classLogger.AddDebug(_description, _extra, _methodCalling);

            AssertAddContentCalledWithLogLevel(expectedLogLevel);
        }

        #endregion

        #region AddInformation Tests

        [Fact]
        public void AddInfo_Given_NoOverrides_ShouldAdd_LogContentAsInformation()
        {
            var expectedLogLevel = LogLevel.Information;

            _classLogger.AddInfo(_description, _extra, _methodCalling);

            AssertAddContentCalledWithLogLevel(expectedLogLevel);
        }

        [Theory]
        [MemberData(nameof(AllLogLevels))]
        public void AddInfo_Given_OnlyNamespaceOverride_ShouldAdd_LogContent_WithNamespaceOverride(LogLevel expectedLogLevel)
        {
            var nameSpaceOverrides = GetNamespaceOverrides(LogLevel.Information, expectedLogLevel);

            _mockLogConfigReader.Setup(_ => _.GetConfigCategory(Category.OverrideLogLevelByNameSpace))
                                .Returns(() => nameSpaceOverrides);

            var classLogger = new ClassLogger(_className, _nameSpaceName, _mockLogMessage.Object, _mockLogConfigReader.Object);

            classLogger.AddInfo(_description, _extra, _methodCalling);

            AssertAddContentCalledWithLogLevel(expectedLogLevel);
        }

        [Theory]
        [MemberData(nameof(AllLogLevels))]
        public void AddInfo_Given_OnlyClassOverride_ShouldAdd_LogContent_WithClassOverride(LogLevel expectedLogLevel)
        {
            var classNameOverrides = GetClassOverrides(LogLevel.Information, expectedLogLevel);

            _mockLogConfigReader.Setup(_ => _.GetConfigSection(Category.OverrideLogLevelByClassName, _className))
                                .Returns(() => classNameOverrides);

            var classLogger = new ClassLogger(_className, _nameSpaceName, _mockLogMessage.Object, _mockLogConfigReader.Object);

            classLogger.AddInfo(_description, _extra, _methodCalling);

            AssertAddContentCalledWithLogLevel(expectedLogLevel);
        }

        [Theory]
        [MemberData(nameof(AllLogLevels))]
        public void AddInfo_Given_NamespaceAndClassOverride_ShouldAdd_LogContent_WithClassOverride(LogLevel expectedLogLevel)
        {
            var classNameOverrides = GetClassOverrides(LogLevel.Information, expectedLogLevel);
            var namespaceOverrides = GetNamespaceOverrides(LogLevel.Information, LogLevel.None);

            _mockLogConfigReader.Setup(_ => _.GetConfigSection(Category.OverrideLogLevelByClassName, _className))
                                .Returns(() => classNameOverrides);

            _mockLogConfigReader.Setup(_ => _.GetConfigCategory(Category.OverrideLogLevelByNameSpace))
                                .Returns(() => namespaceOverrides);

            var classLogger = new ClassLogger(_className, _nameSpaceName, _mockLogMessage.Object, _mockLogConfigReader.Object);

            classLogger.AddInfo(_description, _extra, _methodCalling);

            AssertAddContentCalledWithLogLevel(expectedLogLevel);
        }

        #endregion

        #region AddWarning Tests

        [Fact]
        public void AddWarning_Given_NoOverrides_ShouldAdd_LogContentAsWarning()
        {
            var expectedLogLevel = LogLevel.Warning;

            _classLogger.AddWarning(_description, _extra, _methodCalling);

            AssertAddContentCalledWithLogLevel(expectedLogLevel);
        }

        [Theory]
        [MemberData(nameof(AllLogLevels))]
        public void AddWarning_Given_OnlyNamespaceOverride_ShouldAdd_LogContent_WithNamespaceOverride(LogLevel expectedLogLevel)
        {
            var nameSpaceOverrides = GetNamespaceOverrides(LogLevel.Warning, expectedLogLevel);

            _mockLogConfigReader.Setup(_ => _.GetConfigCategory(Category.OverrideLogLevelByNameSpace))
                                .Returns(() => nameSpaceOverrides);

            var classLogger = new ClassLogger(_className, _nameSpaceName, _mockLogMessage.Object, _mockLogConfigReader.Object);

            classLogger.AddWarning(_description, _extra, _methodCalling);

            AssertAddContentCalledWithLogLevel(expectedLogLevel);
        }

        [Theory]
        [MemberData(nameof(AllLogLevels))]
        public void AddWarning_Given_OnlyClassOverride_ShouldAdd_LogContent_WithClassOverride(LogLevel expectedLogLevel)
        {
            var classNameOverrides = GetClassOverrides(LogLevel.Warning, expectedLogLevel);

            _mockLogConfigReader.Setup(_ => _.GetConfigSection(Category.OverrideLogLevelByClassName, _className))
                                .Returns(() => classNameOverrides);

            var classLogger = new ClassLogger(_className, _nameSpaceName, _mockLogMessage.Object, _mockLogConfigReader.Object);

            classLogger.AddWarning(_description, _extra, _methodCalling);

            AssertAddContentCalledWithLogLevel(expectedLogLevel);
        }

        [Theory]
        [MemberData(nameof(AllLogLevels))]
        public void AddWarning_Given_NamespaceAndClassOverride_ShouldAdd_LogContent_WithClassOverride(LogLevel expectedLogLevel)
        {
            var classNameOverrides = GetClassOverrides(LogLevel.Warning, expectedLogLevel);
            var namespaceOverrides = GetNamespaceOverrides(LogLevel.Warning, LogLevel.None);

            _mockLogConfigReader.Setup(_ => _.GetConfigSection(Category.OverrideLogLevelByClassName, _className))
                                .Returns(() => classNameOverrides);

            _mockLogConfigReader.Setup(_ => _.GetConfigCategory(Category.OverrideLogLevelByNameSpace))
                                .Returns(() => namespaceOverrides);

            var classLogger = new ClassLogger(_className, _nameSpaceName, _mockLogMessage.Object, _mockLogConfigReader.Object);

            classLogger.AddWarning(_description, _extra, _methodCalling);

            AssertAddContentCalledWithLogLevel(expectedLogLevel);
        }

        #endregion

        #region AddError Tests

        [Fact]
        public void AddError_Given_NoOverrides_ShouldAdd_LogContentAsError()
        {
            var expectedLogLevel = LogLevel.Error;

            _classLogger.AddError(_description, _extra, _methodCalling);

            AssertAddContentCalledWithLogLevel(expectedLogLevel);
        }

        [Theory]
        [MemberData(nameof(AllLogLevels))]
        public void AddError_Given_OnlyNamespaceOverride_ShouldAdd_LogContent_WithNamespaceOverride(LogLevel expectedLogLevel)
        {
            var nameSpaceOverrides = GetNamespaceOverrides(LogLevel.Error, expectedLogLevel);

            _mockLogConfigReader.Setup(_ => _.GetConfigCategory(Category.OverrideLogLevelByNameSpace))
                                .Returns(() => nameSpaceOverrides);

            var classLogger = new ClassLogger(_className, _nameSpaceName, _mockLogMessage.Object, _mockLogConfigReader.Object);

            classLogger.AddError(_description, _extra, _methodCalling);

            AssertAddContentCalledWithLogLevel(expectedLogLevel);
        }

        [Theory]
        [MemberData(nameof(AllLogLevels))]
        public void AddError_Given_OnlyClassOverride_ShouldAdd_LogContent_WithClassOverride(LogLevel expectedLogLevel)
        {
            var classNameOverrides = GetClassOverrides(LogLevel.Error, expectedLogLevel);

            _mockLogConfigReader.Setup(_ => _.GetConfigSection(Category.OverrideLogLevelByClassName, _className))
                                .Returns(() => classNameOverrides);

            var classLogger = new ClassLogger(_className, _nameSpaceName, _mockLogMessage.Object, _mockLogConfigReader.Object);

            classLogger.AddError(_description, _extra, _methodCalling);

            AssertAddContentCalledWithLogLevel(expectedLogLevel);
        }

        [Theory]
        [MemberData(nameof(AllLogLevels))]
        public void AddError_Given_NamespaceAndClassOverride_ShouldAdd_LogContent_WithClassOverride(LogLevel expectedLogLevel)
        {
            var classNameOverrides = GetClassOverrides(LogLevel.Error, expectedLogLevel);
            var namespaceOverrides = GetNamespaceOverrides(LogLevel.Error, LogLevel.None);

            _mockLogConfigReader.Setup(_ => _.GetConfigSection(Category.OverrideLogLevelByClassName, _className))
                                .Returns(() => classNameOverrides);

            _mockLogConfigReader.Setup(_ => _.GetConfigCategory(Category.OverrideLogLevelByNameSpace))
                                .Returns(() => namespaceOverrides);

            var classLogger = new ClassLogger(_className, _nameSpaceName, _mockLogMessage.Object, _mockLogConfigReader.Object);

            classLogger.AddError(_description, _extra, _methodCalling);

            AssertAddContentCalledWithLogLevel(expectedLogLevel);
        }

        #endregion

        #region AddCritical Tests

        [Fact]
        public void AddCritical_Given_NoOverrides_ShouldAdd_LogContentAsCritical()
        {
            var expectedLogLevel = LogLevel.Critical;

            _classLogger.AddCritical(_description, _extra, _methodCalling);

            AssertAddContentCalledWithLogLevel(expectedLogLevel);
        }

        [Theory]
        [MemberData(nameof(AllLogLevels))]
        public void AddCritical_Given_OnlyNamespaceOverride_ShouldAdd_LogContent_WithNamespaceOverride(LogLevel expectedLogLevel)
        {
            var nameSpaceOverrides = GetNamespaceOverrides(LogLevel.Critical, expectedLogLevel);

            _mockLogConfigReader.Setup(_ => _.GetConfigCategory(Category.OverrideLogLevelByNameSpace))
                                .Returns(() => nameSpaceOverrides);

            var classLogger = new ClassLogger(_className, _nameSpaceName, _mockLogMessage.Object, _mockLogConfigReader.Object);

            classLogger.AddCritical(_description, _extra, _methodCalling);

            AssertAddContentCalledWithLogLevel(expectedLogLevel);
        }

        [Theory]
        [MemberData(nameof(AllLogLevels))]
        public void AddCritical_Given_OnlyClassOverride_ShouldAdd_LogContent_WithClassOverride(LogLevel expectedLogLevel)
        {
            var classNameOverrides = GetClassOverrides(LogLevel.Critical, expectedLogLevel);

            _mockLogConfigReader.Setup(_ => _.GetConfigSection(Category.OverrideLogLevelByClassName, _className))
                                .Returns(() => classNameOverrides);

            var classLogger = new ClassLogger(_className, _nameSpaceName, _mockLogMessage.Object, _mockLogConfigReader.Object);

            classLogger.AddCritical(_description, _extra, _methodCalling);

            AssertAddContentCalledWithLogLevel(expectedLogLevel);
        }

        [Theory]
        [MemberData(nameof(AllLogLevels))]
        public void AddCritical_Given_NamespaceAndClassOverride_ShouldAdd_LogContent_WithClassOverride(LogLevel expectedLogLevel)
        {
            var classNameOverrides = GetClassOverrides(LogLevel.Critical, expectedLogLevel);
            var namespaceOverrides = GetNamespaceOverrides(LogLevel.Critical, LogLevel.None);

            _mockLogConfigReader.Setup(_ => _.GetConfigSection(Category.OverrideLogLevelByClassName, _className))
                                .Returns(() => classNameOverrides);

            _mockLogConfigReader.Setup(_ => _.GetConfigCategory(Category.OverrideLogLevelByNameSpace))
                                .Returns(() => namespaceOverrides);

            var classLogger = new ClassLogger(_className, _nameSpaceName, _mockLogMessage.Object, _mockLogConfigReader.Object);

            classLogger.AddCritical(_description, _extra, _methodCalling);

            AssertAddContentCalledWithLogLevel(expectedLogLevel);
        }

        #endregion

        #region AddConfiguredLogLevel Tests

        [Fact]
        public void AddConfiguredLogLevel_Given_LogLevelNotConfigured_ShouldAdd_LogContentWithLogLevel_None()
        {
            var expectedLogLevel = LogLevel.None;

            _classLogger.AddConfiguredLogLevel("FakeConfigSection", "FakeConfigKey", _description, _extra, "FakeCategoryKey", _methodCalling);

            AssertAddContentCalledWithLogLevel(expectedLogLevel);
        }

        [Theory]
        [MemberData(nameof(AllLogLevels))]
        public void AddConfiguredLogLevel_Given_LogLevelIsConfigured_ShouldAdd_LogContentWithLogLevel_None(LogLevel expectedLogLevel)
        {
            var configCategoryKey = "CategoryKey";
            var configSectionKey = "SectionKey";
            var configKey = "ConfigKey";

            _mockLogConfigReader.Setup(_ => _.GetLogLevel(configSectionKey, configKey, configCategoryKey)).Returns(() => expectedLogLevel);

            _classLogger.AddConfiguredLogLevel(configSectionKey, configKey, _description, _extra, configCategoryKey, _methodCalling);

            AssertAddContentCalledWithLogLevel(expectedLogLevel);
        }

        #endregion

        #region IncreaseLogLevel Tests

        [Theory]
        [MemberData(nameof(AllLogLevels))]
        public void IncreaseLogLevel_Given_LogLevelNotNone_ShouldIncrease_LogMessageLogLevel_ToLogLevelProvided(LogLevel expectedLogLevel)
        {
            _classLogger.IncreaseLevel(expectedLogLevel, _description, _extra, _methodCalling);

            _mockLogMessage.Verify(_ => 
                _.IncreaseLevel(It.Is<LogContent>(_ =>
                       _.LogLevel == expectedLogLevel
                    && _.ClassName == _className
                    && _.NameSpace == _nameSpaceName
                    && _.MethodCalling == _methodCalling
                    && _.Description == _description
                    && _.Extra as string == _extra
                    )), Times.Once());
        }

        #endregion

        #region Edge Case Tests

        [Fact]
        public void ClassLogger_Given_NamespaceIsNull_ShouldAdd_ContentWithNamespace_Unknown()
        {
            var expectedNamespace = "Unknown";
            var expectedLogLevel = LogLevel.Trace;

            var classLogger = new ClassLogger(_className, namespaceName: null, _mockLogMessage.Object, _mockLogConfigReader.Object);

            classLogger.AddTrace(_description, _extra, _methodCalling);

            AssertAddContentCalledWithLogLevel(expectedLogLevel, _className, expectedNamespace, _methodCalling, _description, _extra);
        }

        #endregion

        #region Private Helper Methods

        /// <summary>
        /// Helper for getting a Dictionary that has an override for a specific key/value.        
        /// <param name="key"></param>
        /// <param name="overrideValue"></param>
        /// <returns></returns>
        private Dictionary<string, LogLevel> GetClassOverrides(LogLevel key, LogLevel overrideValue) =>
            new() { { key.ToString(), overrideValue } };

        /// <summary>
        /// Helper for getting a Dictionary for nameSpace overrides that has an override for a specific key/value.
        /// The namespace parameter defaults to constant for namespace.
        /// </summary>
        private Dictionary<string, Dictionary<string, LogLevel>> GetNamespaceOverrides(
            LogLevel key, LogLevel overrideValue, string nameSpace = _nameSpaceName) =>
                new()
                {
                    {   nameSpace,
                        new()
                        {
                            { key.ToString(), overrideValue }
                        }
                    },
                };

        /// <summary>
        /// Helper for asserting that content is added to LogMessage with expected LogLevel. 
        /// Other values default to constants for className, namespace, methodCalling, description, extra.
        /// expectedNumberOfTimesCalled defaults to 1.
        /// </summary>
        private void AssertAddContentCalledWithLogLevel(LogLevel expectedLogLevel,
            string className = _className, string nameSpace = _nameSpaceName, string methodCalling = _methodCalling,
            string description = _description, string extra = _extra, int expectedTimesCalled = 1) =>
                _mockLogMessage.Verify(_ =>
                   _.AddContent(It.Is<LogContent>(c =>
                          c.LogLevel == expectedLogLevel
                       && c.ClassName == className
                       && c.NameSpace == nameSpace
                       && c.MethodCalling == methodCalling
                       && c.Description == description
                       && c.Extra as string == extra
                       )), Times.Exactly(expectedTimesCalled));
    }
    
    #endregion
}
