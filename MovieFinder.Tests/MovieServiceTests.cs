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
                Title = "������",
                Genre = Genre.SciFi,
                Description = "��� ���� � �����������.",
                Duration = TimeSpan.FromMinutes(148),
                ActorList = new List<ActorDto>
                {
                new ActorDto { FirstName = "��������", LastName = "��������" },
                new ActorDto { FirstName = "������", LastName = "������-������" }
                }

            },
            new MovieDto
            {
                Id = 2,
                Title = "������ ������", 
                Genre = Genre.Action,
                Description = "������ ������ �������.",
                Duration = TimeSpan.FromMinutes(152),
                ActorList = new List<ActorDto>
                {
                    new ActorDto { FirstName = "��������", LastName = "����" },
                    new ActorDto { FirstName = "���", LastName = "������" }
                }
            },
            new MovieDto
            {
                Id = 3,
                Title = "�������",
                Genre = Genre.Romance,
                Description = "������� ����� �� ���� ����������.",
                Duration = TimeSpan.FromMinutes(195),
                ActorList = new List<ActorDto>
                {
                    new ActorDto { FirstName = "��������", LastName = "��������" },
                    new ActorDto { FirstName = "����", LastName = "�������" }
                }
            },
            new MovieDto
            {
                Id = 4,
                Title = "������� ����", 
                Genre = Genre.Drama,
                Description = "����� ��������, �� ������������� ��������.",
                Duration = TimeSpan.FromMinutes(142),
                ActorList = new List<ActorDto>
                {
                    new ActorDto { FirstName = "���", LastName = "�����" },
                    new ActorDto { FirstName = "�����", LastName = "����" }
                }
            },
            new MovieDto
            {
                Id = 5,
                Title = "�������",
                Genre = Genre.SciFi,
                Description = "����������� ���������� � ������ �� �������.",
                Duration = TimeSpan.FromMinutes(136),
                ActorList = new List<ActorDto>
                {
                    new ActorDto { FirstName = "�����", LastName = "����" },
                    new ActorDto { FirstName = "�����-���", LastName = "����" }
                }
            },

            new MovieDto
            {
                Id = 6,
                Title = "��������� �����",
                Genre = Genre.Romance,
                Description = "����� �� ��������",
                Duration = TimeSpan.FromMinutes(110),
                ActorList = new List<ActorDto>
                {
                    new ActorDto { FirstName = "������", LastName = "�������" },
                    new ActorDto { FirstName = "��������", LastName = "��������" }
                }
            }
        };
        #endregion


        [Fact]
        public async Task Search_ByTitle_ReturnsCorrectMovie()  //����� �� ������� �������� ������ ������� ���������� �����
        {
            //Arrange
            var repoMock = new Mock<IMovieRepository>(); 
            repoMock.Setup(r => r.GetAllWithActorsAsync()).ReturnsAsync(_movies); // ��� �������� ��� �����������, ������� ������ ������� ������ �������.
            var service = new MovieService(repoMock.Object); // �������� ��� � ����������� ������ �������.

            //Act
            var result = await service.SearchMoviesAsync("������", null, null);

            //Assert
            Assert.Single(result);                              // ������ ��������� ���� �������
            Assert.Contains(result, m => m.Title == "������");  // � ��������� "������"
        }

        [Fact]
        public async Task Search_ByPartOfTitle_ReturnsCorrectBothMovies()  //����� �� ���������� �������� ������ ������� 2 ���������� ������
        {
            //Arrange
            var repoMock = new Mock<IMovieRepository>();
            repoMock.Setup(r => r.GetAllWithActorsAsync()).ReturnsAsync(_movies); // ��� �������� ��� �����������, ������� ������ ������� ������ �������.
            var service = new MovieService(repoMock.Object); // �������� ��� � ����������� ������ �������.

            //Act
            var result = await service.SearchMoviesAsync("���", null, null);

            //Assert
            Assert.Equal(2,result.Count());                             // ������ ��������� ��� ������
            Assert.Contains(result, m => m.Title == "������");
            Assert.Contains(result, m => m.Title == "��������� �����");
        }

        [Fact]
        public async Task Search_ByGenre_ReturnsCorrectBothMovies()  //����� �� ����� SciFi ������ ��� ������
        {
            //Arrange
            var repoMock = new Mock<IMovieRepository>();
            repoMock.Setup(r => r.GetAllWithActorsAsync()).ReturnsAsync(_movies); // ��� �������� ��� �����������, ������� ������ ������� ������ �������.
            var service = new MovieService(repoMock.Object); // �������� ��� � ����������� ������ �������.

            //Act
            var result = await service.SearchMoviesAsync(null, Genre.SciFi, null);

            //Assert
            Assert.Equal(2, result.Count());                             // ������ ��������� ��� ������
        }

        [Fact]
        public async Task Search_ByActor_ReturnsCorrectThreeMovies()  //����� �� �������� ������ 3 ������
        {
            //Arrange
            var repoMock = new Mock<IMovieRepository>();
            repoMock.Setup(r => r.GetAllWithActorsAsync()).ReturnsAsync(_movies); // ��� �������� ��� �����������, ������� ������ ������� ������ �������.
            var service = new MovieService(repoMock.Object); // �������� ��� � ����������� ������ �������.

            //Act
            var result = await service.SearchMoviesAsync(null, null, "��������");

            //Assert
            Assert.Equal(3, result.Count());                             // ������ ��������� ��� ������
        }

        [Fact]
        public async Task Search_ByActorAndByGenre_ReturnsCorrectMovie()  //����� �� �������� � SciFi ������ ���� �����
        {
            //Arrange
            var repoMock = new Mock<IMovieRepository>();
            repoMock.Setup(r => r.GetAllWithActorsAsync()).ReturnsAsync(_movies); // ��� �������� ��� �����������, ������� ������ ������� ������ �������.
            var service = new MovieService(repoMock.Object); // �������� ��� � ����������� ������ �������.

            //Act
            var result = await service.SearchMoviesAsync(null, Genre.SciFi, "��������");

            //Assert
            Assert.Single(result);
            Assert.Contains(result, m => m.Title == "������"); //������ ��������� ����� "������" � ��������
        }


        [Fact]
        public async Task SearchMovies_EmptyList_ReturnsEmptyCollection()  //����� ������� �� ������� ������ ������� ������ ������ ���������
        {
            //Arrange
            List<MovieDto> emptyList = new();
            var repoMock = new Mock<IMovieRepository>();
            repoMock.Setup(r => r.GetAllWithActorsAsync()).ReturnsAsync(emptyList); // ��� �������� ��� �����������, ������� ������ ������� ������ �������.
            var service = new MovieService(repoMock.Object); // �������� ��� � ����������� ������ �������.

            //Act
            var result = await service.SearchMoviesAsync("������", null, null);

            //Assert
            Assert.Empty(result);   // �� ������ ������ ���������
        }

        [Fact]
        public async Task SearchMovies_WithNullArguments_ReturnsAllMovies()  //����� ������� �� ������ ���������� ������ �������� ������ �������
        {
            //Arrange
            var repoMock = new Mock<IMovieRepository>();
            repoMock.Setup(r => r.GetAllWithActorsAsync()).ReturnsAsync(_movies); // ��� �������� ��� �����������, ������� ������ ������� ������ �������.
            var service = new MovieService(repoMock.Object); // �������� ��� � ����������� ������ �������.

            //Act
            var result = await service.SearchMoviesAsync(null, null, null);

            //Assert
            Assert.Equal(_movies.Count(),result.Count());   // ������ �������� ������
        }

    }
}