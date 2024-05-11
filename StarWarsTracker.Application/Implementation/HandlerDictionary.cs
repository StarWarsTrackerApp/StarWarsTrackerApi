using StarWarsTracker.Domain.Extensions;

namespace StarWarsTracker.Application.Implementation
{
    /// <summary>
    /// This class implements the IHandlerDictionary interface to expose Handlers for Requests.
    /// The Key is the Type of IRequest or IRequestResponse. 
    /// The Value is the Type of Handler that will handle the Request.
    /// </summary>
    internal class HandlerDictionary : Dictionary<Type, Type>, IHandlerDictionary
    {
        private HandlerDictionary() { }

        public static IHandlerDictionary FromDictionaryAssembly() => FromAssemblyOf(typeof(HandlerDictionary));

        public static IHandlerDictionary FromAssemblyOf<T>(T sourceType) where T : Type
        {
            var dictionary = new HandlerDictionary();
            var handlerType = typeof(IHandler<>);

            var handlerDefinition = typeof(IHandler<>).GetGenericTypeDefinition();

            foreach (var type in sourceType.Assembly.GetTypes())
            {
                if (type.ImplementsGenericArguments(handlerDefinition) && type.IsClass && !type.IsAbstract)
                {
                    var requestType = type.GetInterface(handlerType.Name)!.GenericTypeArguments.Single();

                    dictionary.Add(requestType, type);
                }
            }

            return dictionary;
        }

        #region Public IHandlerDictionary Method

        public bool TryGetHandlerType<TRequest>(TRequest request, out Type handler) where TRequest : class =>
                   TryGetValue(request.GetType(), out handler!);

        #endregion
    }
}
