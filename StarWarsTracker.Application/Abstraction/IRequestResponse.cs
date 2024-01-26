namespace StarWarsTracker.Application.Abstraction
{
    /// <summary>
    /// This interface will be implemented by any Request (class) that returns a response which will be fetched by the IOrchestrator.
    /// </summary>
    public interface IRequestResponse<TResponse> { }
}
