using StarWarsTracker.Application.Requests.EventRequests.GetByNameLike;
using StarWarsTracker.Domain.Constants;
using StarWarsTracker.Tests.Shared.Helpers;

namespace StarWarsTracker.Application.Tests.RequestTests.EventRequestTests.GetEventByNameLikeTests
{
    public class GetEventsByNameLikeRequestTests
    {
        [Theory]
        [InlineData(MaxLength.EventName)]
        [InlineData(MaxLength.EventName - 10)]
        [InlineData(MaxLength.EventName - 20)]
        [InlineData(MaxLength.EventName - 30)]
        public void GetEventsByNameLikeRequest_Given_NameIsMaxLengthOrLess_IsValid_ShouldReturn_True(int nameLength)
        {
            var request = new GetEventsByNameLikeRequest(StringHelper.RandomString(nameLength));

            Assert.True(request.IsValid(out _));
        }

        [Theory]
        [InlineData(MaxLength.EventName + 1)]
        [InlineData(MaxLength.EventName + 10)]
        [InlineData(MaxLength.EventName + 20)]
        [InlineData(MaxLength.EventName + 30)]
        public void GetEventsByNameLikeRequest_Given_NameIsMoreThanMaxLength_IsValid_ShouldReturn_False(int nameLength)
        {
            var request = new GetEventsByNameLikeRequest(StringHelper.RandomString(nameLength));

            Assert.False(request.IsValid(out _));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("  ")]
        [InlineData("   ")]
        public void GetEventsByNameLikeRequest_Given_NameIsNullEmptyOrWhiteSpace_IsValid_ShouldReturn_False(string name)
        {
            var request = new GetEventsByNameLikeRequest(name);

            Assert.False(request.IsValid(out _));
        }
    }
}
