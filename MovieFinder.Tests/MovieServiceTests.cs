using Moq;
using MovieFinder.Logic.Interfaces;
using MovieFinder.Logic.Models;
using MovieFinder.Logic.Services;
using Xunit;

namespace MovieFinder.Tests
{
    public class MovieServiceTests
    {

        [Fact]
        public async Task SearchMoviesAsync_ForwardsParameters_ToRepository() //Проверяем вызывается ли метод в репо через метод в сервисе
        {
            var repo = new Mock<IMovieRepository>();
            repo.Setup(r => r.SearchMoviesAsync("abc", Genre.Drama, "tom")).ReturnsAsync(Array.Empty<MovieDto>());

            var service = new MovieService(repo.Object);

            await service.SearchMoviesAsync("abc", Genre.Drama, "tom");

            repo.Verify(r => r.SearchMoviesAsync("abc", Genre.Drama, "tom"), Times.Once); 
        }

    }
}