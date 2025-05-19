using Microsoft.EntityFrameworkCore;
using MovieFinder.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            modelBuilder.Entity<Movie>()
                .HasMany(m => m.Actors)                // Один актер имеет много фильмов
                .WithMany(a => a.Movies);             // Каждый фильм связан с множеством актеров
        }
    }
}
