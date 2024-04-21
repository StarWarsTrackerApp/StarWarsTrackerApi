using StarWarsTracker.Logging.Abstraction;

namespace StarWarsTracker.Logging.Implementation
{
    internal class ClassLoggerFactory : IClassLoggerFactory
    {
        #region Private Members

        private readonly ILogMessage _logMessage;

        private readonly ILogConfigReader _logConfigReader;

        #endregion

        #region Constructor

        public ClassLoggerFactory(ILogMessage logMessage, ILogConfigReader logConfigReader)
        {
            _logMessage = logMessage;
            _logConfigReader = logConfigReader;
        }

        #endregion

        #region Public IClassLoggerFactory Method

        public IClassLogger GetLoggerFor<T>(T classToLogFor) where T : class
        {
            var type = classToLogFor.GetType();

            return new ClassLogger(type.Name, type.Namespace, _logMessage, _logConfigReader);
        }

        #endregion
    }
}
