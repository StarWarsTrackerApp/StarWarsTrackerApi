namespace StarWarsTracker.Persistence.DataTransferObjects
{
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
    }
}
