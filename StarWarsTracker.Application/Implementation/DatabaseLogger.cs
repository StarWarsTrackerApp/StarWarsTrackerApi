using StarWarsTracker.Domain.Constants.LogConfigs;
using StarWarsTracker.Logging.Abstraction;
using StarWarsTracker.Persistence.DataRequestObjects.Logging;
using System.Text.Json;

namespace StarWarsTracker.Application.Implementation
{
    /// <summary>
    /// This class implements the ILogWriter to save LogMessages to the database.
    /// </summary>
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
            var minimumLoggingLevel = _logConfigReader.GetLogLevel(Section.DatabaseLogSettings, Key.LogMessageLevelToWrite);
            var contentLevel = _logConfigReader.GetLogLevel(Section.DatabaseLogSettings, Key.LogContentLevelToWrite);

            if (minimumLoggingLevel == LogLevel.None)
            {
                return;
            }
            
            var logLevel = logMessage.GetLevel();

            var logContents = logMessage.GetContent(contentLevel ?? LogLevel.None);
                
            var message = new
            {
                ElapsedMilliseconds = logMessage.GetElapsedMilliseconds(),
                LogLevel = logLevel,
                NameOfLogLevel = logLevel.ToString(),
                LogContents = logContents
            };

            var messageJson = JsonSerializer.Serialize(message);

            if (minimumLoggingLevel <= logLevel)
            {
                Task.Run(() => _dataAccess.ExecuteAsync(new InsertLog((int)logLevel, messageJson, requestPath, httpMethod, null)));
            }
        }

        #endregion
    }
}
