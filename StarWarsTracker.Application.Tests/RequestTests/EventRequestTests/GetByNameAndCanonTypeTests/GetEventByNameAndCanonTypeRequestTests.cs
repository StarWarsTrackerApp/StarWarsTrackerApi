using StarWarsTracker.Application.Requests.EventRequests.GetByNameAndCanonType;
using StarWarsTracker.Domain.Constants;
using StarWarsTracker.Domain.Enums;
using StarWarsTracker.Tests.Shared.Helpers;

namespace StarWarsTracker.Application.Tests.RequestTests.EventRequestTests.GetByNameAndCanonTypeTests
{
    public class GetEventByNameAndCanonTypeRequestTests
    {
        #region IsValid Name Tests

        [Theory]
        [InlineData(MaxLength.EventName)]
        [InlineData(MaxLength.EventName - 10)]
        [InlineData(MaxLength.EventName - 20)]
        [InlineData(MaxLength.EventName - 30)]
        public void GetEventByNameAndCanonTypeRequest_Given_NameIsMaxLengthOrLess_IsValid_ShouldReturn_True(int nameLength)
        {
            var request = new GetEventByNameAndCanonTypeRequest(TestString.Random(nameLength), CanonType.StrictlyCanon);

            Assert.True(request.IsValid(out _));
        }

        [Theory]
        [InlineData(MaxLength.EventName + 1)]
        [InlineData(MaxLength.EventName + 10)]
        [InlineData(MaxLength.EventName + 20)]
        [InlineData(MaxLength.EventName + 30)]
        public void GetEventByNameAndCanonTypeRequest_Given_NameIsMoreThanMaxLength_IsValid_ShouldReturn_False(int nameLength)
        {
            var request = new GetEventByNameAndCanonTypeRequest(TestString.Random(nameLength), CanonType.StrictlyCanon);

            Assert.False(request.IsValid(out _));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("  ")]
        [InlineData("   ")]
        public void GetEventByNameAndCanonTypeRequest_Given_NameIsNullEmptyOrWhiteSpace_IsValid_ShouldReturn_False(string name)
        {
            var request = new GetEventByNameAndCanonTypeRequest(name, CanonType.StrictlyCanon);

            Assert.False(request.IsValid(out _));
        }

        #endregion

        #region IsValid CanonType Tests

        [Theory]
        [InlineData(CanonType.StrictlyCanon)]
        [InlineData(CanonType.StrictlyLegends)]
        [InlineData(CanonType.CanonAndLegends)]
        public void GetEventByNameAndCanonTypeRequest_Given_CanonTypeIsValid_IsValid_ShouldReturn_True(CanonType canonType)
        {
            var request = new GetEventByNameAndCanonTypeRequest(TestString.Random(), canonType);

            Assert.True(request.IsValid(out _));
        }

        [Theory]
        [InlineData(int.MinValue)]
        [InlineData(0)]
        [InlineData(int.MaxValue)]
        public void GetEventByNameAndCanonTypeRequest_Given_CanonTypeIsNotValid_IsValid_ShouldReturn_False(int canonType)
        {
            var request = new GetEventByNameAndCanonTypeRequest(TestString.Random(), (CanonType)canonType);

            Assert.False(request.IsValid(out _));
        }

        #endregion
    }
}
