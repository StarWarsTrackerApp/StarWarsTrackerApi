using StarWarsTracker.Domain.Logging;
using StarWarsTracker.Persistence.DataRequestObjects.Logging;

namespace StarWarsTracker.Application.Implementation
{
    public class DatabaseLogger: ILogger
    {
        #region Private Members

        private readonly IDataAccess _dataAccess;

        private readonly ILogConfig _logConfig;

        #endregion

        #region Constructor

        public DatabaseLogger(ILogConfig logConfig, IDataAccess dataAccess)
        {
            _logConfig = logConfig;

            _dataAccess = dataAccess;
        }

        #endregion

        #region Public Method

        public void Log(ILogMessage message, string route, string method)
        {
            var minimumLoggingLevel = _logConfig.GetLogLevel(LogConfigSection.DatabaseLogger, LogConfigKey.LogLevel);
            var contentLevel = _logConfig.GetLogLevel(LogConfigSection.DatabaseLogger, LogConfigKey.LogDetails);

            if (minimumLoggingLevel == LogLevel.None)
            {
                return;
            }

            var logLevel = message.GetLogLevel();

            var content = message.GetLogsAsJson(contentLevel);

            if (minimumLoggingLevel <= logLevel)
            {
                Task.Run(() => _dataAccess.ExecuteAsync(new InsertLog((int)logLevel, content, route, method, null)));
            }
        }

        #endregion
    }
}
