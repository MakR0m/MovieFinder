using MovieFinder.Logic.Interfaces;
using MovieFinder.Logic.Models;

namespace MovieFinder.Logic.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _repository;

        public MovieService(IMovieRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<MovieDto>> SearchMoviesAsync(string? title, Genre? genre, string? actorName)
        {
            return await _repository.SearchMoviesAsync(title, genre, actorName);
        }

        public async Task<IEnumerable<MovieDto>> GetAllWithActorsAsync()   //Получить список фильмов с актерами
        {
            return await _repository.GetAllWithActorsAsync();             //Вызов метода получения списка фильмов с актерами из репозитория
        }
    }
}
