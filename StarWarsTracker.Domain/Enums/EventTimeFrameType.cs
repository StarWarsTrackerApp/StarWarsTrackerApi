namespace StarWarsTracker.Domain.Enums
{
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
