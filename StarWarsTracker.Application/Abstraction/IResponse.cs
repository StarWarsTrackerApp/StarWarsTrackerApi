namespace StarWarsTracker.Application.Abstraction
{
    /// <summary>
    /// This interface defines the contract for any response object that is returned by a handler.
    /// </summary>
    public interface IResponse
    {
        /// <summary>
        /// Returns the Status Code that is associated with the response. The StatusCode should be a valid HTTPStatusCode.
        /// </summary>
        /// <returns>int representing the HTTPStatusCode for the response.</returns>
        public int GetStatusCode();

        /// <summary>
        /// Returns the ResponseBody that is associated with the response. Returns null if no response body.
        /// </summary>
        /// <returns> the ResponseBody that is associated with the response. Returns null if no response body.</returns>
        public object? GetBody();
    }
}
