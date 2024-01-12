namespace StarWarsTracker.Application.Abstraction
{
    internal interface IHandlerDictionary
    {
        /// <summary>
        /// This method takes in the requestType and returns the Type of IHandler that handles the requestType.
        /// </summary>
        public Type? GetHandlerType(Type requestType);
    }
}
