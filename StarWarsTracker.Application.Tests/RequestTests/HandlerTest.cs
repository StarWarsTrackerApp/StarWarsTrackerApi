using Moq;
using StarWarsTracker.Application.Abstraction;
using StarWarsTracker.Persistence.Abstraction;

namespace StarWarsTracker.Application.Tests.RequestTests
{
    public abstract class HandlerTest
    {
        protected Mock<IDataAccess> _mockDataAccess;

        protected Mock<IOrchestrator> _mockOrchestrator;

        public HandlerTest()
        {
            _mockDataAccess = new Mock<IDataAccess>();

            _mockOrchestrator = new Mock<IOrchestrator>();
        }

        #region Helpers for setting up Mock calls to database with IDataAccess

        protected void SetupMockFetchAsync<TRequest, TResponse>(TResponse? response) where TRequest : IDataFetch<TResponse> =>
            _mockDataAccess.Setup(_ => _.FetchAsync(It.IsAny<TRequest>())).Returns(Task.FromResult(response));

        protected void SetupMockFetchListAsync<TRequest, TResponse>(IEnumerable<TResponse> response) where TRequest : IDataFetch<TResponse> =>
            _mockDataAccess.Setup(_ => _.FetchListAsync(It.IsAny<TRequest>())).Returns(Task.FromResult(response));

        protected void SetupMockExecuteAsync<TRequest>(int response) where TRequest : IDataExecute =>
            _mockDataAccess.Setup(_ => _.ExecuteAsync(It.IsAny<TRequest>())).Returns(Task.FromResult(response));

        #endregion

        #region Helpers for setting up Mock calls to IOrchestrator

        protected void SetupMockGetRequestResponseAsync<TRequest, TResponse>(TResponse? response) where TRequest : IRequestResponse<TResponse> =>
            _mockOrchestrator.Setup(_ => _.GetRequestResponseAsync(It.IsAny<TRequest>())).Returns(Task.FromResult(response!));

        #endregion
    }
}
