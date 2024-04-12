using StarWarsTracker.Domain.Constants.LogConfigs;
using StarWarsTracker.Logging.Abstraction;
using StarWarsTracker.Persistence.DataRequestObjects.Logging;

namespace StarWarsTracker.Application.Implementation
{
    public class DatabaseLogger: ILogWriter
    {
        #region Private Members

        private readonly IDataAccess _dataAccess;

        private readonly ILogConfigReader _logConfigReader;

        #endregion

        #region Constructor

        public DatabaseLogger(ILogConfigReader logConfigReader, IDataAccess dataAccess)
        {
            _logConfigReader = logConfigReader;

            _dataAccess = dataAccess;
        }

        #endregion

        #region Public Method

        public void Write(ILogMessage logMessage, string requestPath, string httpMethod)
        {
            var minimumLoggingLevel = _logConfigReader.GetCustomLogLevel(Section.DatabaseLogSettings, Key.LogMessageLevelToWrite);
            var contentLevel = _logConfigReader.GetCustomLogLevel(Section.DatabaseLogSettings, Key.LogContentLevelToWrite);

            if (minimumLoggingLevel == LogLevel.None)
            {
                return;
            }

            var logLevel = logMessage.GetLevel();

            var content = logMessage.GetMessageJson(contentLevel ?? LogLevel.None);

            if (minimumLoggingLevel <= logLevel)
            {
                Task.Run(() => _dataAccess.ExecuteAsync(new InsertLog((int)logLevel, content, requestPath, httpMethod, null)));
            }
        }

        #endregion
    }
}
