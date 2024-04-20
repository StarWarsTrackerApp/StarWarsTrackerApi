using System.ComponentModel;

namespace StarWarsTracker.Domain.Extensions
{
    public static class EnumExtensions
    {
        public static string GetEnumDescription(this Enum e)
        {
            var field = e.GetType().GetField(e.ToString());

            if(field != null && Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) is DescriptionAttribute attribute)
            {
                return attribute.Description;
            }

            return e.ToString();
        }
    }
}
