using StarWarsTracker.Application.Abstraction;
using StarWarsTracker.Application.BaseObjects.BaseHandlers;
using StarWarsTracker.Logging.Abstraction;

namespace StarWarsTracker.Application.Tests.ImplementationTests.TestRequests
{
    internal class ExampleRequest : IRequest { }

    internal class ExampleRequestHandler : BaseRequestHandler<ExampleRequest>
    {
        public ExampleRequestHandler(IClassLoggerFactory classLoggerFactory) : base(classLoggerFactory)
        {
        }

        public override Task ExecuteRequestAsync(ExampleRequest request) => Task.CompletedTask;
    }
}
