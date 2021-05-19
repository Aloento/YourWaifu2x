using System;
using System.ComponentModel;
using System.Reflection;

namespace YourWaifu2x
{
    public static class EnumHelper
    {
        public static T GetAttribute<T>(this Enum @enum) where T : Attribute
        {
            return @enum.GetType()
                .GetField(@enum.ToString())
                .GetCustomAttribute<T>();
        }

        /// <summary>
        /// Get the description value from DescriptionAttribute
        /// </summary>
        public static string GetDescription(this Enum @enum)
        {
            return @enum.GetAttribute<DescriptionAttribute>()?.Description;
        }
    }
}
