using StarWarsTracker.Domain.Logging;

namespace StarWarsTracker.Application.Implementation
{
    public class LoggerFactory : ILoggerFactory
    {
        #region Private Members

        private readonly LogSettings _logSettings;

        private readonly IDataAccess _dataAccess;

        #endregion

        #region Constructor

        public LoggerFactory(LogSettings settings, IDataAccess dataAccess)
        {
            _logSettings = settings;
            _dataAccess = dataAccess;
        }

        #endregion

        #region Public Method

        public ILogger<T> NewLogger<T>()
        {
            return new Logger<T>(_logSettings.LogLevel, _dataAccess);
        }

        #endregion

        #region LogSettings Record

        public record LogSettings(LogLevel LogLevel);
        
        #endregion
    }
}
