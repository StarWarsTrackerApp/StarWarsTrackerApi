namespace StarWarsTracker.Application.Abstraction
{
    /// <summary>
    /// This interface defines the contract for the implementation to instantiate an object at run time.
    /// </summary>
    internal interface ITypeActivator
    {
        /// <summary>
        /// Returns a new instance of the typeToInstantiate provided casted as the TResponse defined.
        /// </summary>
        /// <typeparam name="TResponse">The Type to cast the object as when it is instantiated.</typeparam>
        /// <param name="typeToInstantiate">the Type of object to be instantiated.</param>
        /// <returns>The instantiated object of the typeToInstantiate provided, cast as the a type of TResponse</returns>
        public TResponse Instantiate<TResponse>(Type typeToInstantiate);
    }
}
