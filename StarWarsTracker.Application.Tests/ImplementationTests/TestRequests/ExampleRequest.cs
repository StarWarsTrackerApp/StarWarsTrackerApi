using StarWarsTracker.Application.Abstraction;
using StarWarsTracker.Application.BaseObjects.BaseHandlers;

namespace StarWarsTracker.Application.Tests.ImplementationTests.TestRequests
{
    internal class ExampleRequest : IRequest { }

    internal class ExampleRequestHandler : BaseRequestHandler<ExampleRequest>
    {
        public override Task ExecuteRequestAsync(ExampleRequest request) => Task.CompletedTask;
    }
}
