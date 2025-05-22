using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace MovieFinder.Data.Data
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)   //Фабрика для создания AppDbContext, чтобы можно было работать с командами ef 
        {                                                    //без DI в design-time.
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlite("Filename=movies.db");

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
