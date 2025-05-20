using Moq;
using MovieFinder.Logic.Interfaces;
using MovieFinder.Logic.Models;
using MovieFinder.Logic.Services;
using Xunit;

namespace MovieFinder.Tests
{
    public class MovieServiceTests
    {
        #region TestMoviesData
        private readonly List<MovieDto> _movies = new()
        {
            new MovieDto
            {
                Id = 1,
                Title = "Начало",
                Genre = Genre.SciFi,
                Description = "Мир снов и подсознания.",
                Duration = TimeSpan.FromMinutes(148),
                ActorList = new List<ActorDto>
                {
                new ActorDto { FirstName = "Леонардо", LastName = "ДиКаприо" },
                new ActorDto { FirstName = "Джозеф", LastName = "Гордон-Левитт" }
                }

            },
            new MovieDto
            {
                Id = 2,
                Title = "Темный рыцарь", 
                Genre = Genre.Action,
                Description = "Бэтмен против Джокера.",
                Duration = TimeSpan.FromMinutes(152),
                ActorList = new List<ActorDto>
                {
                    new ActorDto { FirstName = "Кристиан", LastName = "Бэйл" },
                    new ActorDto { FirstName = "Хит", LastName = "Леджер" }
                }
            },
            new MovieDto
            {
                Id = 3,
                Title = "Титаник",
                Genre = Genre.Romance,
                Description = "История любви на фоне катастрофы.",
                Duration = TimeSpan.FromMinutes(195),
                ActorList = new List<ActorDto>
                {
                    new ActorDto { FirstName = "Леонардо", LastName = "ДиКаприо" },
                    new ActorDto { FirstName = "Кейт", LastName = "Уинслет" }
                }
            },
            new MovieDto
            {
                Id = 4,
                Title = "Форрест Гамп", 
                Genre = Genre.Drama,
                Description = "Жизнь простого, но удивительного человека.",
                Duration = TimeSpan.FromMinutes(142),
                ActorList = new List<ActorDto>
                {
                    new ActorDto { FirstName = "Том", LastName = "Хэнкс" },
                    new ActorDto { FirstName = "Робин", LastName = "Райт" }
                }
            },
            new MovieDto
            {
                Id = 5,
                Title = "Матрица",
                Genre = Genre.SciFi,
                Description = "Виртуальная реальность и борьба за свободу.",
                Duration = TimeSpan.FromMinutes(136),
                ActorList = new List<ActorDto>
                {
                    new ActorDto { FirstName = "Киану", LastName = "Ривз" },
                    new ActorDto { FirstName = "Кэрри-Энн", LastName = "Мосс" }
                }
            },

            new MovieDto
            {
                Id = 6,
                Title = "Начальная школа",
                Genre = Genre.Romance,
                Description = "Какое то описание",
                Duration = TimeSpan.FromMinutes(110),
                ActorList = new List<ActorDto>
                {
                    new ActorDto { FirstName = "Сергей", LastName = "Бурунов" },
                    new ActorDto { FirstName = "Леонардо", LastName = "ДиКаприо" }
                }
            }
        };
        #endregion


        [Fact]
        public async Task Search_ByTitle_ReturnsCorrectMovie()  //Поиск по полному названию должен вернуть корректный фильм
        {
            //Arrange
            var repoMock = new Mock<IMovieRepository>(); 
            repoMock.Setup(r => r.GetAllWithActorsAsync()).ReturnsAsync(_movies); // Мок заглушка для репозитория, которая должна вернуть список фильмов.
            var service = new MovieService(repoMock.Object); // Передаем мок в тестируемый сервис фильмов.

            //Act
            var result = await service.SearchMoviesAsync("Начало", null, null);

            //Assert
            Assert.Single(result);                              // Должен вернуться один элемент
            Assert.Contains(result, m => m.Title == "Начало");  // с названием "Начало"
        }

        [Fact]
        public async Task Search_ByPartOfTitle_ReturnsCorrectBothMovies()  //Поиск по частичному названию должен вернуть 2 корректных фильма
        {
            //Arrange
            var repoMock = new Mock<IMovieRepository>();
            repoMock.Setup(r => r.GetAllWithActorsAsync()).ReturnsAsync(_movies); // Мок заглушка для репозитория, которая должна вернуть список фильмов.
            var service = new MovieService(repoMock.Object); // Передаем мок в тестируемый сервис фильмов.

            //Act
            var result = await service.SearchMoviesAsync("нач", null, null);

            //Assert
            Assert.Equal(2,result.Count());                             // Должно вернуться два фильма
            Assert.Contains(result, m => m.Title == "Начало");
            Assert.Contains(result, m => m.Title == "Начальная школа");
        }

        [Fact]
        public async Task Search_ByGenre_ReturnsCorrectBothMovies()  //Поиск по жанру SciFi вернет два фильма
        {
            //Arrange
            var repoMock = new Mock<IMovieRepository>();
            repoMock.Setup(r => r.GetAllWithActorsAsync()).ReturnsAsync(_movies); // Мок заглушка для репозитория, которая должна вернуть список фильмов.
            var service = new MovieService(repoMock.Object); // Передаем мок в тестируемый сервис фильмов.

            //Act
            var result = await service.SearchMoviesAsync(null, Genre.SciFi, null);

            //Assert
            Assert.Equal(2, result.Count());                             // Должно вернуться два фильма
        }

        [Fact]
        public async Task Search_ByActor_ReturnsCorrectThreeMovies()  //Поиск по ДиКаприо вернет 3 фильма
        {
            //Arrange
            var repoMock = new Mock<IMovieRepository>();
            repoMock.Setup(r => r.GetAllWithActorsAsync()).ReturnsAsync(_movies); // Мок заглушка для репозитория, которая должна вернуть список фильмов.
            var service = new MovieService(repoMock.Object); // Передаем мок в тестируемый сервис фильмов.

            //Act
            var result = await service.SearchMoviesAsync(null, null, "леонардо");

            //Assert
            Assert.Equal(3, result.Count());                             // Должно вернуться три фильма
        }

        [Fact]
        public async Task Search_ByActorAndByGenre_ReturnsCorrectMovie()  //Поиск по ДиКаприо и SciFi вернет один фильм
        {
            //Arrange
            var repoMock = new Mock<IMovieRepository>();
            repoMock.Setup(r => r.GetAllWithActorsAsync()).ReturnsAsync(_movies); // Мок заглушка для репозитория, которая должна вернуть список фильмов.
            var service = new MovieService(repoMock.Object); // Передаем мок в тестируемый сервис фильмов.

            //Act
            var result = await service.SearchMoviesAsync(null, Genre.SciFi, "Дикаприо");

            //Assert
            Assert.Single(result);
            Assert.Contains(result, m => m.Title == "Начало"); //Должен вернуться фильм "Начало" с ДиКаприо
        }


        [Fact]
        public async Task SearchMovies_EmptyList_ReturnsEmptyCollection()  //Поиск фильмов из пустого списка фильмов вернет пустую коллекцию
        {
            //Arrange
            List<MovieDto> emptyList = new();
            var repoMock = new Mock<IMovieRepository>();
            repoMock.Setup(r => r.GetAllWithActorsAsync()).ReturnsAsync(emptyList); // Мок заглушка для репозитория, которая должна вернуть список фильмов.
            var service = new MovieService(repoMock.Object); // Передаем мок в тестируемый сервис фильмов.

            //Act
            var result = await service.SearchMoviesAsync("Начало", null, null);

            //Assert
            Assert.Empty(result);   // Не должно ничего вернуться
        }

        [Fact]
        public async Task SearchMovies_WithNullArguments_ReturnsAllMovies()  //Поиск фильмов по пустым аргументам вернет исходный список фильмов
        {
            //Arrange
            var repoMock = new Mock<IMovieRepository>();
            repoMock.Setup(r => r.GetAllWithActorsAsync()).ReturnsAsync(_movies); // Мок заглушка для репозитория, которая должна вернуть список фильмов.
            var service = new MovieService(repoMock.Object); // Передаем мок в тестируемый сервис фильмов.

            //Act
            var result = await service.SearchMoviesAsync(null, null, null);

            //Assert
            Assert.Equal(_movies.Count(),result.Count());   // Вернет исходный список
        }

    }
}