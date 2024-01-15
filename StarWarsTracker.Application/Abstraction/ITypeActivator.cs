namespace StarWarsTracker.Application.Abstraction
{
    internal interface ITypeActivator
    {
        public TResponse Instantiate<TResponse>(Type typeToInstantiate);
    }
}
