using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MovieFinder.Data.Entity;

namespace MovieFinder.Data.Data
{
    public class AppDbContext : DbContext // Наследуемся от DbContext — базового класса Entity Framework Core для работы с базой данных
    {
        public DbSet<Movie> Movies => Set<Movie>();   // Представление таблицы "Movies" в бд.
        public DbSet<Actor> Actors => Set<Actor>();   // Представление таблицы "Actors" в бд.

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var secondsConverter = new ValueConverter<TimeSpan, int>(    
            v => (int)v.TotalSeconds,                                    //Хранить TimeSpan в секундах в int   
            v => TimeSpan.FromSeconds(v));                               //Забирать int в TimeSpan в секундах

            modelBuilder.Entity<Movie>()
                .HasMany(m => m.Actors)                // Один актер имеет много фильмов
                .WithMany(a => a.Movies);             // Каждый фильм связан с множеством актеров

            modelBuilder.Entity<Movie>()              //Применение конвертера для задания типа int таблицы фильмов свойства Duration
                .Property(m => m.Duration)
                .HasConversion(secondsConverter)
                .HasColumnType("INTEGER");
        }
    }
}
