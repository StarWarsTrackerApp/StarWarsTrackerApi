using Moq;
using StarWarsTracker.Persistence.Abstraction;

namespace StarWarsTracker.Application.Tests.RequestTests
{
    public abstract class HandlerTest
    {
        protected Mock<IDataAccess> _mockDataAccess;

        public HandlerTest()
        {
            _mockDataAccess = new Mock<IDataAccess>();
        }

        protected void SetupMockFetchAsync<TRequest, TResponse>(TResponse? response) where TRequest : IDataFetch<TResponse> =>
            _mockDataAccess.Setup(_ => _.FetchAsync(It.IsAny<TRequest>())).Returns(Task.FromResult(response));
       
        protected void SetupMockFetchListAsync<TRequest, TResponse>(IEnumerable<TResponse> response) where TRequest : IDataFetch<TResponse> =>
            _mockDataAccess.Setup(_ => _.FetchListAsync(It.IsAny<TRequest>())).Returns(Task.FromResult(response));

        protected void SetupMockExecuteAsync<TRequest>(int response) where TRequest : IDataExecute =>
            _mockDataAccess.Setup(_ => _.ExecuteAsync(It.IsAny<TRequest>())).Returns(Task.FromResult(response));
    }
}
