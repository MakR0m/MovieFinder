using MovieFinder.Logic.Models;

namespace MovieFinder.Logic.Interfaces
{
    public interface IMovieService  // Интерфейс для взаимодействия с сервисом фильмов в UI 
    {
        Task<IEnumerable<MovieDto>>SearchMoviesAsync(string? tittle, Genre? genre, string? actorName);   //Поиск фильма по имени, жанру, актеру
        Task<IEnumerable<MovieDto>> GetAllWithActorsAsync(); // Получить список всех фильмов (с include актеров)
        
    }
}
