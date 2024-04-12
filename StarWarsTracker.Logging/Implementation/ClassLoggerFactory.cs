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

        /// <summary>
        /// Return an IClassLogger that uses the T classToLogFor to set the ClassName and Namespace for the ClassLogger.
        /// </summary>
        /// <typeparam name="T">The type of class that the IClassLogger returned will be logging messages for. </typeparam>
        /// <param name="classToLogFor">An instance of the class that the IClassLogger will be logging messages for. </param>
        /// <returns>An IClassLogger configured to log messages for the T classToLogFor provided. </returns>
        public IClassLogger GetLoggerFor<T>(T classToLogFor) where T : class
        {
            var type = classToLogFor.GetType();

            return new ClassLogger(type.Name, type.Namespace, _logMessage, _logConfigReader);
        }

        #endregion
    }
}
