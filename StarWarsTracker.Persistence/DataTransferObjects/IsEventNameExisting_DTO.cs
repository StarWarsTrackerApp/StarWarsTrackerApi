using StarWarsTracker.Domain.Enums;

namespace StarWarsTracker.Persistence.DataTransferObjects
{
    /// <summary>
    /// DTO for SQL Query to determine if an Event.Name exists in Canon, Legends, or CanonAndLegends
    /// </summary>
    public class IsEventNameExisting_DTO
    {
        public IsEventNameExisting_DTO() { }

        public IsEventNameExisting_DTO(bool nameExistsInStrictlyCanon, bool nameExistsInStrictlyLegends, bool nameExistsInCanonAndLegends)
        {
            NameExistsInStrictlyCanon = nameExistsInStrictlyCanon;
            NameExistsInStrictlyLegends = nameExistsInStrictlyLegends;
            NameExistsInCanonAndLegends = nameExistsInCanonAndLegends;
        }

        public bool NameExistsInStrictlyCanon{ get; set; }

        public bool NameExistsInStrictlyLegends{ get; set; }

        public bool NameExistsInCanonAndLegends{ get; set; }

        public bool IsExistingInCanonType(CanonType canonType)
        {
            return canonType switch
            {
                CanonType.StrictlyCanon => NameExistsInStrictlyCanon || NameExistsInCanonAndLegends,
                CanonType.StrictlyLegends => NameExistsInStrictlyLegends || NameExistsInCanonAndLegends,
                CanonType.CanonAndLegends => NameExistsInCanonAndLegends || NameExistsInStrictlyCanon || NameExistsInStrictlyLegends,
                _ => throw new NotImplementedException(),
            };
        }
    }
}
