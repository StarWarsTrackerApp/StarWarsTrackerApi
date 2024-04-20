using System.ComponentModel;

namespace StarWarsTracker.Domain.Enums
{
    public enum CanonType
    {
        [Description("Canon")]
        StrictlyCanon = 1,
        
        [Description("Legends")]
        StrictlyLegends = 2,
        
        [Description("Canon & Legends")]
        CanonAndLegends = 3
    }
}
