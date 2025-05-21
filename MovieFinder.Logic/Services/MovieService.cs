using MovieFinder.Logic.Interfaces;
using MovieFinder.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieFinder.Logic.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _repository;

        public MovieService(IMovieRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<MovieDto>> SearchMoviesAsync(string? tittle, Genre? genre, string? actorName)
        {
            var movies = await _repository.GetAllWithActorsAsync();
            if (!string.IsNullOrWhiteSpace(tittle)) 
                movies = movies.Where(m => m.Title.Contains(tittle, StringComparison.OrdinalIgnoreCase));  // Вернуть фильмы в название которых содержит указанную строку без учета регистра
            if (genre.HasValue)
                movies = movies.Where(m => m.Genre == genre.Value);
            if (!string.IsNullOrWhiteSpace (actorName))
                movies = movies.Where(m => m.ActorList                                                    //Вернуть список фильмов у которых список актеров
                    .Any(a => a.GetFullName().Contains(actorName, StringComparison.OrdinalIgnoreCase)));  //содержит указанную строку с именем актера без учета регистра
            return movies;
        }

        public async Task<IEnumerable<MovieDto>> GetAllWithActorsAsync()   //Получить список фильмов с актерами
        {
            return await _repository.GetAllWithActorsAsync();             //Вызов метода получения списка фильмов с актерами из репозитория
        }
    }
}
