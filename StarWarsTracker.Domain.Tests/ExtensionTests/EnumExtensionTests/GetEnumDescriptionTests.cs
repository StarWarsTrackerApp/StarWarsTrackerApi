using StarWarsTracker.Domain.Extensions;
using System.ComponentModel;

namespace StarWarsTracker.Domain.Tests.ExtensionTests.EnumExtensionTests
{
    public class GetEnumDescriptionTests
    {
        private const string TestEnumDescription = "Description Value For Test Enum With Description";

        private enum TestEnum
        {
            [Description(TestEnumDescription)]
            WithDescription,
            WithoutDescription
        }

        [Fact]
        public void GetEnumDescription_Given_EnumHasDescription_ShouldReturn_EnumDescription()
        {
            var result = TestEnum.WithDescription.GetEnumDescription();

            Assert.Equal(TestEnumDescription, result);
        }

        [Fact]
        public void GetEnumDescription_Given_EnumHasNoDescription_ShouldReturn_EnumToString()
        {
            var expected = TestEnum.WithoutDescription.ToString();

            var result = TestEnum.WithoutDescription.GetEnumDescription();

            Assert.Equal(expected, result);
        }
    }
}
