using MovieFinder.Data.Repositories;
using MovieFinder.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieFinder.Tests.IntegrationTests
{
    public class EfMovieRepositoryTests : IClassFixture<SqliteFixture>
    {
        private readonly SqliteFixture _fx;
        private readonly EfMovieRepository _repo;

        public EfMovieRepositoryTests(SqliteFixture fx)
        {
            _fx = fx;
            _repo = new EfMovieRepository(_fx.DbContext);
        }

        [Fact]
        public async Task Search_ByTitle_ReturnsCorrectMovie()  //Поиск по полному названию должен вернуть корректный фильм
        {
            //Arrange
            await _fx.ResetAndSeedAsync();

            //Act
            var result = await _repo.SearchMoviesAsync("Начало", null, null);

            //Assert
            Assert.Single(result);                              // Должен вернуться один элемент
            Assert.Contains(result, m => m.Title == "Начало");  // с названием "Начало"
        }

        [Fact]
        public async Task Search_ByPartOfTitle_ReturnsCorrectBothMovies()  //Поиск по частичному названию должен вернуть 2 корректных фильма
        {
            //Arrange
            await _fx.ResetAndSeedAsync();

            //Act
            var result = await _repo.SearchMoviesAsync("Нач", null, null);

            //Assert
            Assert.Equal(2, result.Count());                             // Должно вернуться два фильма
            Assert.Contains(result, m => m.Title == "Начало");
            Assert.Contains(result, m => m.Title == "Начальная школа");
        }

        [Fact]
        public async Task Search_ByGenre_ReturnsCorrectBothMovies()  //Поиск по жанру SciFi вернет два фильма
        {
            //Arrange
            await _fx.ResetAndSeedAsync();

            //Act
            var result = await _repo.SearchMoviesAsync(null, Genre.SciFi, null);

            //Assert
            Assert.Equal(2, result.Count());                             // Должно вернуться два фильма
        }

        [Fact]
        public async Task Search_ByActor_ReturnsCorrectThreeMovies()  //Поиск по ДиКаприо вернет 3 фильма
        {
            //Arrange
            await _fx.ResetAndSeedAsync();

            //Act
            var result = await _repo.SearchMoviesAsync(null, null, "Леонардо");

            //Assert
            Assert.Equal(3, result.Count());                             // Должно вернуться три фильма
        }

        [Fact]
        public async Task Search_ByActorAndByGenre_ReturnsCorrectMovie()  //Поиск по ДиКаприо и SciFi вернет один фильм
        {
            //Arrange
            await _fx.ResetAndSeedAsync();

            //Act
            var result = await _repo.SearchMoviesAsync(null, Genre.SciFi, "ДиКаприо");

            //Assert
            Assert.Single(result);
            Assert.Contains(result, m => m.Title == "Начало"); //Должен вернуться фильм "Начало" с ДиКаприо
        }


        [Fact]
        public async Task SearchMovies_EmptyList_ReturnsEmptyCollection()  //Поиск фильмов из пустого списка фильмов вернет пустую коллекцию
        {
            //Arrange
            await _fx.ClearAsync();

            //Act
            var result = await _repo.SearchMoviesAsync("Начало", null, null);

            //Assert
            Assert.Empty(result);   // Не должно ничего вернуться
        }

        [Fact]
        public async Task SearchMovies_WithNullArguments_ReturnsAllMovies()  //Поиск фильмов по пустым аргументам вернет исходный список фильмов
        {
            //Arrange
            await _fx.ResetAndSeedAsync();

            //Act
            var result = await _repo.SearchMoviesAsync(null, null, null);

            //Assert
            Assert.Equal(_fx.DbContext.Movies.Count(), result.Count());   // Вернет исходный список
        }
    }
}
