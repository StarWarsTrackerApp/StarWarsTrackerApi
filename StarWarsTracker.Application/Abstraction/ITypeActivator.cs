namespace StarWarsTracker.Application.Abstraction
{
    internal interface ITypeActivator
    {
        /// <summary>
        /// returns a new instance of the typeToInstantiate provided casted as the TResponse defined.
        /// </summary>
        public TResponse Instantiate<TResponse>(Type typeToInstantiate);
    }
}
