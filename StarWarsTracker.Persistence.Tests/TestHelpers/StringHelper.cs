using System.Text;

namespace StarWarsTracker.Persistence.Tests.TestHelpers
{
    public static class StringHelper
    {        
        public static string RandomString(int length = 36)
        {
            var characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890-=_)(*&^%$#@!";

            var sb = new StringBuilder(length);

            for (int i = 0; i < length; i++)
            {
                var randomIndex = new Random().Next(0, characters.Length);

                sb.Append(characters[randomIndex]);
            }

            return sb.ToString();
        }
    }
}
