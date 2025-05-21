using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieFinder.Mobile.Services
{
    public class PosterService  // Сервис для определения постера к фильму
    {
        private readonly Dictionary<string, string> _posterMap = new()
        {
            { "Начало", "posters/inception.jpg" },
            { "Темный рыцарь", "posters/dark_knight.jpg" },
            { "Титаник", "posters/titanic.jpg" },
            { "Матрица", "posters/matrix.jpg" },
            { "Форрест Гамп", "posters/forrest.jpg" },
            { "Начальная школа", "posters/school.jpg" },
        };

        public string GetPosterPath(string title) => 
            _posterMap.TryGetValue(title, out var posterPath) ? posterPath : "posters/default.jpg"; //получаем значением енума по названию фильма, если не удается - возвращаем путь к дефолтному постеру.
    }
}
