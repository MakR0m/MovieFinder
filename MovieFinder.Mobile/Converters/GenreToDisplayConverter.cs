using MovieFinder.Logic.Extensions;
using MovieFinder.Logic.Models;
using System.Globalization;

namespace MovieFinder.Mobile.Converters
{
    public class GenreToDisplayConverter : IValueConverter  //Конвертер для отображения перечисления жанров на русском языке (по описанию)
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Genre genre)
            {
                return genre.GetDescription();
            }
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
