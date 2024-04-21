using System.ComponentModel;

namespace StarWarsTracker.Domain.Enums
{
    /// <summary>
    /// This Enum defines the different Continuities for Star Wars.
    /// </summary>
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
