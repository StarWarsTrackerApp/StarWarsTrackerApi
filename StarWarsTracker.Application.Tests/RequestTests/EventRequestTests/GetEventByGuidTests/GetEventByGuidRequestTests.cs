using StarWarsTracker.Application.Requests.EventRequests.GetByGuid;

namespace StarWarsTracker.Application.Tests.RequestTests.EventRequestTests.GetEventByGuidTests
{
    public class GetEventByGuidRequestTests
    {
        [Fact]
        public void GetEventByGuidRequest_Given_EventGuidNotSet_IsValid_ShouldReturn_False()
        {
            var request = new GetEventByGuidRequest();

            Assert.False(request.IsValid(out _));
        }

        [Fact]
        public void GetEventByGuidRequest_Given_EventGuidIsEmpty_IsValid_ShouldReturn_False()
        {
            var request = new GetEventByGuidRequest(Guid.Empty);

            Assert.False(request.IsValid(out _));
        }

        [Fact]
        public void GetEventByGuidRequest_Given_EventGuidIsValidGuid_IsValid_ShouldReturn_True()
        {
            var request = new GetEventByGuidRequest(Guid.NewGuid());

            Assert.True(request.IsValid(out _));
        }
    }
}
