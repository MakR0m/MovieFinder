using Microsoft.EntityFrameworkCore;
using MovieFinder.Data.Data;
using MovieFinder.Data.Entity;
using MovieFinder.Logic.Interfaces;
using MovieFinder.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieFinder.Data.Repositories
{
    public class EfMovieRepository /*: IMovieRepository*/
    {
        private readonly AppDbContext _context;

        public EfMovieRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MovieDto>> GetAllWithActorsAsync()
        {
            var movies = await _context.Movies.Include(x => x.Actors).ToListAsync(); // Возвращаем список фильмов с актерами
            return movies.Select(MapToDto).ToList(); // Преобразуем объекты - сущности в объекты бизнес логики. Select перебирает элементы и преобразует каждый объект в новый и возвращает список объектов.
        }

        private static MovieDto MapToDto(Movie entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            return new MovieDto
            {
                Id = entity.Id,
                Title = entity.Title,
                Genre = Enum.TryParse<Genre>(entity.Genre, out var g) ? g : Genre.Unknown,
                Description = entity.Description,
                Duration = entity.Duration,
                ActorList = entity.Actors.Select(MapToDto).ToList(),  //Аналогично преобразуем список сущностей актеров в список сущностей дто. (List<Actor> был IEnumerable то не надо было бы)
            };
        }

        private static ActorDto MapToDto(Actor entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            return new ActorDto
            {
                FirstName = entity.FirstName,
                LastName = entity.LastName,
            };
        }

    }
}
