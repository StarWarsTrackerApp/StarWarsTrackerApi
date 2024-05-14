namespace StarWarsTracker.Application.Abstraction
{
    public interface IHandler<TRequest>
    {
        public Task<IResponse> GetResponseAsync(TRequest request);
    }
}
