using System.ComponentModel;

namespace StarWarsTracker.Domain.Extensions
{
    public static class EnumExtensions
    {
        /// <summary>
        /// If an Enum has a DescriptionAttribute then return the value of the Description, else return the value.ToString()
        /// </summary>
        /// <param name="e">The Enum value to get a description for.</param>
        /// <returns>The Enum's DescriptionAttribute.Value if available, else the Enum Value.ToString() </returns>
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
