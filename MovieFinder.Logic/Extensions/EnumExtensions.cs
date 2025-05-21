using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

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
