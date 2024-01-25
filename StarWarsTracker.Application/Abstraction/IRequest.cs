namespace StarWarsTracker.Application.Abstraction
{
    /// <summary>
    /// This interface will be implemented by any Request (class) that will be executed by the IOrchestrator and does not return a response.
    /// </summary>
    public interface IRequest { }

    /// <summary>
    /// This interface will be implemented by any Request (class) that returns a response which will be fetched by the IOrchestrator.
    /// </summary>
    public interface IRequestResponse<TResponse> { }
}
