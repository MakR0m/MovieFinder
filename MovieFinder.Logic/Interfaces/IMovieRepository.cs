using MovieFinder.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieFinder.Logic.Interfaces
{
    public interface IMovieRepository // Интерфейс для взаимодействия с репозиторием фильмов проекта Data
    {
        Task<IEnumerable<MovieDto>> GetAllWithActorsAsync(); // Получить коллекцию фильмов и связанных с ними актеров
    }
}
