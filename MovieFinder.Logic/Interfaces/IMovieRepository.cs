using MovieFinder.Logic.Models;

namespace MovieFinder.Logic.Interfaces
{
    public interface IMovieRepository // Интерфейс для взаимодействия с репозиторием фильмов проекта Data
    {
        Task<IEnumerable<MovieDto>> GetAllWithActorsAsync(); // Получить коллекцию фильмов и связанных с ними актеров
    }
}
