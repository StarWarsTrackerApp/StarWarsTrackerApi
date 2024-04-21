namespace StarWarsTracker.Logging.Abstraction
{
    /// <summary>
    /// This interface defines the contract for how a LogMessage will be written/saved
    /// </summary>
    public interface ILogWriter
    {
        /// <summary>
        /// Write/Save the LogMessage.
        /// </summary>
        /// <param name="logMessage">LogMessage to be saved.</param>
        /// <param name="requestPath">The requestPath for the endpoint the LogMessage is for.</param>
        /// <param name="httpMethod">The httpMethod for the endpoint the LogMessage is for.</param>
        public void Write(ILogMessage logMessage, string requestPath, string httpMethod);
    }
}
