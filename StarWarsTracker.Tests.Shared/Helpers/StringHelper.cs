using System.Text;

namespace StarWarsTracker.Tests.Shared.Helpers
{
    public static class StringHelper
    {
        private const string _characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890-=_)(*&^%$#@!";

        public static string RandomString(int length = 36)
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
