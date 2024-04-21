namespace StarWarsTracker.Logging.Abstraction
{
    /// <summary>
    /// This interface defines the contract for the Factory that will return an IClassLogger for different classes to use for logging.
    /// </summary>
    public interface IClassLoggerFactory
    {
        /// <summary>
        /// Return an IClassLogger that uses the T classToLogFor to set the ClassName and Namespace for the ClassLogger.
        /// </summary>
        /// <typeparam name="T">The type of class that the IClassLogger returned will be logging messages for. </typeparam>
        /// <param name="classToLogFor">An instance of the class that the IClassLogger will be logging messages for. </param>
        /// <returns>An IClassLogger configured to log messages for the T classToLogFor provided. </returns>
        public IClassLogger GetLoggerFor<T>(T classToLogFor) where T : class;
    }
}
