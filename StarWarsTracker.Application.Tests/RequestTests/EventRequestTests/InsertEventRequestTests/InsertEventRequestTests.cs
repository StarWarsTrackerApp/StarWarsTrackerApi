using StarWarsTracker.Application.Requests.EventRequests.Insert;
using StarWarsTracker.Domain.Constants;
using StarWarsTracker.Domain.Enums;
using StarWarsTracker.Tests.Shared.Helpers;

namespace StarWarsTracker.Application.Tests.RequestTests.EventRequestTests.InsertEventRequestTests
{
    public class InsertEventRequestTests
    {
        #region IsValid Name Tests

        [Theory]
        [InlineData(MaxLength.EventName)]
        [InlineData(MaxLength.EventName - 10)]
        [InlineData(MaxLength.EventName - 20)]
        [InlineData(MaxLength.EventName - 30)]
        public void InsertEventRequest_Given_NameIsMaxLengthOrLess_IsValid_ShouldReturn_True(int nameLength)
        {
            var request = new InsertEventRequest()
            {
                Name = StringHelper.RandomString(nameLength),
                Description = StringHelper.RandomString(),
                CanonType = CanonType.StrictlyCanon
            };

            Assert.True(request.IsValid(out _));
        }

        [Theory]
        [InlineData(MaxLength.EventName + 1)]
        [InlineData(MaxLength.EventName + 10)]
        [InlineData(MaxLength.EventName + 20)]
        [InlineData(MaxLength.EventName + 30)]
        public void InsertEventRequest_Given_NameIsMoreThanMaxLength_IsValid_ShouldReturn_False(int nameLength)
        {
            var request = new InsertEventRequest()
            {
                Name = StringHelper.RandomString(nameLength),
                Description = StringHelper.RandomString(),
                CanonType = CanonType.StrictlyCanon
            };

            Assert.False(request.IsValid(out _));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("  ")]
        [InlineData("   ")]
        public void InsertEventRequest_Given_NameIsNullEmptyOrWhiteSpace_IsValid_ShouldReturn_False(string name)
        {
            var request = new InsertEventRequest()
            {
                Name = name,
                Description = StringHelper.RandomString(),
                CanonType = CanonType.StrictlyCanon
            };

            Assert.False(request.IsValid(out _));
        }

        #endregion

        #region IsValid Description Tests

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("  ")]
        [InlineData("   ")]
        public void InsertEventRequest_Given_DescriptionIsNullEmptyOrWhiteSpace_IsValid_ShouldReturn_False(string description)
        {
            var request = new InsertEventRequest()
            {
                Name = StringHelper.RandomString(),
                Description = description,
                CanonType = CanonType.StrictlyCanon
            };

            Assert.False(request.IsValid(out _));
        }

        [Theory]
        [InlineData("Description")]
        [InlineData("Can")]
        [InlineData("Be")]
        [InlineData("Any Length")]
        [InlineData("There Is No Limit To Description Length, As Long As It Is Not NULL, Empty, Or WhiteSpace!")]
        public void InsertEventRequest_Given_DescriptionIsNotNullEmptyOrWhiteSpace_IsValid_ShouldReturn_True(string description)
        {
            var request = new InsertEventRequest()
            {
                Name = StringHelper.RandomString(),
                Description = description,
                CanonType = CanonType.StrictlyCanon
            };

            Assert.True(request.IsValid(out _));
        }

        #endregion

        #region IsValid CanonType Tests

        [Theory]
        [InlineData(CanonType.StrictlyCanon)]
        [InlineData(CanonType.StrictlyLegends)]
        [InlineData(CanonType.CanonAndLegends)]
        public void InsertEventRequest_Given_CanonTypeIsValid_IsValid_ShouldReturn_True(CanonType canonType)
        {
            var request = new InsertEventRequest()
            {
                Name = StringHelper.RandomString(),
                Description = StringHelper.RandomString(),
                CanonType = canonType
            };

            Assert.True(request.IsValid(out _));
        }

        [Theory]
        [InlineData(int.MinValue)]
        [InlineData(0)]
        [InlineData(int.MaxValue)]
        public void InsertEventRequest_Given_CanonTypeIsNotValid_IsValid_ShouldReturn_False(int canonType)
        {
            var request = new InsertEventRequest()
            {
                Name = StringHelper.RandomString(),
                Description = StringHelper.RandomString(),
                CanonType = (CanonType)canonType
            };

            Assert.False(request.IsValid(out _));
        }

        #endregion
    }
}
