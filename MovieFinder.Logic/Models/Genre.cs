using System.ComponentModel;

namespace MovieFinder.Logic.Models
{
    public enum Genre
    {
        [Description ("Не определен")]
        Unknown = 0,
        [Description("Боевик")]
        Action,
        [Description("Комедия")]
        Comedy,
        [Description("Драма")]
        Drama,
        [Description("Ужасы")]
        Horror,
        [Description("Сай-фай")]
        SciFi,
        [Description("Романтика")]
        Romance,
        [Description("Триллер")]
        Thriller,
        [Description("Мультфильм")]
        Animation,
        [Description("Документальный")]
        Documentary
    }
}
