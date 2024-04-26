using StarWarsTracker.Domain.Enums;
using StarWarsTracker.Domain.Models;
using System.Diagnostics.CodeAnalysis;

namespace StarWarsTracker.Api.Examples
{
    [ExcludeFromCodeCoverage]
    public static class ExampleModel
    {
        /// <summary>
        /// Example Event with a random Guid.
        /// </summary>
        public static Event Event => new(Guid.NewGuid(), "Name Of Event", "Description Of Event", CanonType.StrictlyCanon);

        /// <summary>
        /// Example EventTimeFrame set at a Definitive Date.
        /// </summary>
        public static EventTimeFrame EventTimeFrame => new(new EventDate(EventDateType.Definitive, -10, 45));
    }
}
