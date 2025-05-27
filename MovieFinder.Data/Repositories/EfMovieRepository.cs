using Microsoft.EntityFrameworkCore;
using MovieFinder.Data.Data;
using MovieFinder.Data.Entity;
using MovieFinder.Logic.Interfaces;
using MovieFinder.Logic.Models;

namespace MovieFinder.Data.Repositories
{
    public class EfMovieRepository : IMovieRepository
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

        private static MovieDto MapToDto(Movie entity) //Маппер модели сущности в модель дто (бизнес-логики)
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

        public async Task<IEnumerable<MovieDto>> SearchMoviesAsync(string? title, Genre? genre, string? actorName)
        {
            var query = _context.Movies
                .Include(m => m.Actors)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(title))
                query = query.Where(m => EF.Functions.Like(m.Title, $"%{title}%"));

            if (genre.HasValue)
                query = query.Where(m => m.Genre == genre.Value.ToString());

            if (!string.IsNullOrWhiteSpace(actorName))
                query = query.Where(m => m.Actors.Any(a => EF.Functions.Like((a.FirstName + " " + a.LastName), $"%{actorName}%")));

            var result = await query.ToListAsync();

            return result.Select(MapToDto).ToList();
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
