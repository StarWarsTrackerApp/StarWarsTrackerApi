namespace StarWarsTracker.Application.Abstraction
{
    internal interface IHandlerFactory
    {
        public IBaseHandler NewHandler<TRequest>(TRequest request);
    }
}
