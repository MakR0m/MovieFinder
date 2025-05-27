using MovieFinder.Data.Data;
using MovieFinder.Data.Entity;
using Microsoft.EntityFrameworkCore;


namespace MovieFinder.Tests.IntegrationTests
{
    public class SqliteFixture : IAsyncLifetime
    {
        public AppDbContext DbContext { get; private set; } = null!;

        #region TestedData
        private readonly List<Movie> _testedData = new()
        {
            new Movie
            {
                Id = 1,
                Title = "Начало",
                Genre = "SciFi",         
                Description = "Мир снов и подсознания.",
                Duration = TimeSpan.FromMinutes(148),
                Actors = new()
                {
                    new Actor { FirstName = "Леонардо", LastName = "ДиКаприо" },
                    new Actor { FirstName = "Джозеф",   LastName = "Гордон-Левитт" }
                }
            },
            new Movie
            {
                Id = 2,
                Title = "Темный рыцарь",
                Genre = "Action",
                Description = "Бэтмен против Джокера.",
                Duration  = TimeSpan.FromMinutes(152),
                Actors = new()
                {
                    new Actor { FirstName = "Кристиан", LastName = "Бэйл" },
                    new Actor { FirstName = "Хит",      LastName = "Леджер" }
                }
            },
            new Movie
            {
                Id = 3,
                Title = "Титаник",
                Genre = "Romance",
                Description = "История любви на фоне катастрофы.",
                Duration = TimeSpan.FromMinutes(195),
                Actors = new()
                {
                    new Actor { FirstName = "Леонардо", LastName = "ДиКаприо" },
                    new Actor { FirstName = "Кейт",     LastName = "Уинслет" }
                }
            },
            new Movie
            {
                Id = 4,
                Title = "Форрест Гамп",
                Genre = "Drama",
                Description = "Жизнь простого, но удивительного человека.",
                Duration = TimeSpan.FromMinutes(142),
                Actors = new()
                {
                    new Actor { FirstName = "Том",   LastName = "Хэнкс" },
                    new Actor { FirstName = "Робин", LastName = "Райт" }
                }
            },
            new Movie
            {
                Id = 5,
                Title = "Матрица",
                Genre = "SciFi",
                Description = "Виртуальная реальность и борьба за свободу.",
                Duration = TimeSpan.FromMinutes(136),
                Actors = new()
                {
                    new Actor { FirstName = "Киану",     LastName = "Ривз" },
                    new Actor { FirstName = "Кэрри-Энн", LastName = "Мосс" }
                }
            },
            new Movie
            {
                Id = 6,
                Title = "Начальная школа",
                Genre = "Romance",
                Description = "Какое-то описание.",
                Duration = TimeSpan.FromMinutes(110),
                Actors = new()
                {
                    new Actor { FirstName = "Сергей",   LastName = "Бурунов" },
                    new Actor { FirstName = "Леонардо", LastName = "ДиКаприо" }
                }
            }
        };
        #endregion

        //Создаём in-memory БД и выполняем первичное наполнение
        public async Task InitializeAsync()
        {
            var opts = new DbContextOptionsBuilder<AppDbContext>()
                       .UseSqlite("Filename=:memory:")          // база только в RAM
                       .Options;

            DbContext = new AppDbContext(opts);
            await DbContext.Database.OpenConnectionAsync();          // требуется для in-memory SQLite
            await DbContext.Database.EnsureCreatedAsync();           // создаём таблицы

            await ResetAndSeedAsync();                         // первое заполнение
        }

        public async Task DisposeAsync()  //Закрываем соединение и освобождаем контекст
        {
            await DbContext.Database.CloseConnectionAsync();
            await DbContext.DisposeAsync();
        }

        public async Task ClearAsync()
        {
            DbContext.Movies.RemoveRange(DbContext.Movies);
            await DbContext.SaveChangesAsync();
        }

        public async Task ResetAndSeedAsync() //Очистить и заполнить заново тестовыми данными
        {
            await ClearAsync();
            await DbContext.Movies.AddRangeAsync(_testedData);
            await DbContext.SaveChangesAsync();
        }
    }
}
