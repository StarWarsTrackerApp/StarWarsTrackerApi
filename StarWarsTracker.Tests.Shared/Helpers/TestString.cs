using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace StarWarsTracker.Tests.Shared.Helpers
{
    [ExcludeFromCodeCoverage]
    public static class TestString
    {
        private const string _characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890-=_)(*&^%$#@!";

        public static string Random(int length = 36)
        {
            var sb = new StringBuilder(length);

            for (int i = 0; i < length; i++)
            {
                var randomIndex = new Random().Next(0, _characters.Length);

                sb.Append(_characters[randomIndex]);
            }

            return sb.ToString();
        }
    }
}
