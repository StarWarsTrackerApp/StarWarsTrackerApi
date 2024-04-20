namespace StarWarsTracker.Domain.Enums
{
    /// <summary>
    /// This enum defines the type of time frame that an Event has.
    /// </summary>
    public enum EventTimeFrameType
    {
        Invalid = 0,

        DefinitiveTime = 1,

        DefinitiveStartDefinitiveEnd = 2,

        DefinitiveStartSpeculativeEnd = 3,

        SpeculativeStartDefinitiveEnd = 4,

        SpeculativeStartSpeculativeEnd = 5
    }
}
