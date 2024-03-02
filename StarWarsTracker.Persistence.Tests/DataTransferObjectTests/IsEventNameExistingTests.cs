using StarWarsTracker.Domain.Enums;
using StarWarsTracker.Persistence.DataTransferObjects;

namespace StarWarsTracker.Persistence.Tests.DataTransferObjectTests
{
    public class IsEventNameExistingTests
    {
        [Fact]
        public void IsEventNameExisting_IsExistingInCanonType_Given_InvalidCanonType_ShouldThrow_NotImplementedException()
        {
            var dto = new IsEventNameExisting_DTO(true, true, true);

            var canonType = (CanonType)0;

            Assert.Throws<NotImplementedException>(() => dto.IsExistingInCanonType(canonType));
        }

        [Theory]
        [MemberData(nameof(IsEventNameExistingTestCases))]
        public void IsEventNameExisting_IsExistingInCanonType_Given_CanonType_ShouldReturn_ExpectedOutput(IsEventNameExisting_DTO dto, CanonType canonType, bool expected)
        {
            var result = dto.IsExistingInCanonType(canonType);

            Assert.Equal(expected, result);
        }

        public static IEnumerable<object[]> IsEventNameExistingTestCases = new[]
        {
            new object[]
            {
                new IsEventNameExisting_DTO(nameExistsInStrictlyCanon: true, nameExistsInStrictlyLegends: false, nameExistsInCanonAndLegends: false),
                CanonType.StrictlyCanon, true
            },
            new object[]
            {
                new IsEventNameExisting_DTO(nameExistsInStrictlyCanon: false, nameExistsInStrictlyLegends: false, nameExistsInCanonAndLegends: true),
                CanonType.StrictlyCanon, true
            },
            new object[]
            {
                new IsEventNameExisting_DTO(nameExistsInStrictlyCanon: false, nameExistsInStrictlyLegends: true, nameExistsInCanonAndLegends: false),
                CanonType.StrictlyCanon, false
            },
            new object[]
            {
                new IsEventNameExisting_DTO(nameExistsInStrictlyCanon: false, nameExistsInStrictlyLegends: false, nameExistsInCanonAndLegends: false),
                CanonType.StrictlyCanon, false
            },
            new object[]
            {
                new IsEventNameExisting_DTO(nameExistsInStrictlyCanon: false, nameExistsInStrictlyLegends: true, nameExistsInCanonAndLegends: false),
                CanonType.StrictlyLegends, true
            },
            new object[]
            {
                new IsEventNameExisting_DTO(nameExistsInStrictlyCanon: false, nameExistsInStrictlyLegends: false, nameExistsInCanonAndLegends: true),
                CanonType.StrictlyLegends, true
            },
            new object[]
            {
                new IsEventNameExisting_DTO(nameExistsInStrictlyCanon: false, nameExistsInStrictlyLegends: false, nameExistsInCanonAndLegends: false),
                CanonType.StrictlyLegends, false
            },
            new object[]
            {
                new IsEventNameExisting_DTO(nameExistsInStrictlyCanon: true, nameExistsInStrictlyLegends: false, nameExistsInCanonAndLegends: false),
                CanonType.StrictlyLegends, false
            },
            new object[]
            {
                new IsEventNameExisting_DTO(nameExistsInStrictlyCanon: true, nameExistsInStrictlyLegends: false, nameExistsInCanonAndLegends: false),
                CanonType.CanonAndLegends, true
            },
            new object[]
            {
                new IsEventNameExisting_DTO(nameExistsInStrictlyCanon: false, nameExistsInStrictlyLegends: true, nameExistsInCanonAndLegends: false),
                CanonType.CanonAndLegends, true
            },
            new object[]
            {
                new IsEventNameExisting_DTO(nameExistsInStrictlyCanon: false, nameExistsInStrictlyLegends: false, nameExistsInCanonAndLegends: true),
                CanonType.CanonAndLegends, true
            },
            new object[]
            {
                new IsEventNameExisting_DTO(nameExistsInStrictlyCanon: false, nameExistsInStrictlyLegends: false, nameExistsInCanonAndLegends: false),
                CanonType.CanonAndLegends, false
            }
        };
    }
}
