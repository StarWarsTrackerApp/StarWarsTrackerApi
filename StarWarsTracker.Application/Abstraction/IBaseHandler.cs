namespace StarWarsTracker.Application.Abstraction
{
    /// <summary>
    /// This BaseHandler is implemented by the IRequestHandler and IRequestResponseHandler, which defines a common method that is used to handle a request.
    /// </summary>
    internal interface IBaseHandler 
    { 
        public Task<object?> HandleAsync(object request);
    }
}
