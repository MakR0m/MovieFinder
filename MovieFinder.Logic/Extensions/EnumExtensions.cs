using System.ComponentModel;
using System.Reflection;

namespace MovieFinder.Logic.Extensions
{
    public static class EnumExtensions  //Класс для расширения функционала перечислений
    {
        public static string GetDescription(this Enum value)   // Позволяет вернуть описание элементов перечисления
        {
            var field = value.GetType().GetField(value.ToString());
            return field?.GetCustomAttribute<DescriptionAttribute>()?.Description ?? value.ToString();
        }
    }
}
