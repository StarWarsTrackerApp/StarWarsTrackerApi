using StarWarsTracker.Application.Abstraction;
using StarWarsTracker.Application.BaseObjects.BaseHandlers;
using StarWarsTracker.Logging.Abstraction;

namespace StarWarsTracker.Application.Tests.ImplementationTests.TestRequests
{
    internal class ExampleRequest { }

    internal class ExampleRequestHandler : BaseHandler<ExampleRequest>
    {
        public ExampleRequestHandler(IClassLoggerFactory classLoggerFactory) : base(classLoggerFactory) { }

        internal protected override Task<IResponse> HandleRequestAsync(ExampleRequest request) => Task.FromResult(Response.Success());
    }
}
