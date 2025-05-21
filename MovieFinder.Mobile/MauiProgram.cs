using Microsoft.Extensions.Logging;
using MovieFinder.Data.Data;
using Microsoft.EntityFrameworkCore;
using System;
using MovieFinder.Logic.Interfaces;
using MovieFinder.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using MovieFinder.Logic.Services;
using MovieFinder.Mobile.ViewModels;

namespace MovieFinder.Mobile
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });
            builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite("Filename=movies.db")); //Создает DbContext (главный объект EF core для взаимодействия с бд)
            builder.Services.TryAddScoped<IMovieRepository,EfMovieRepository>(); //Один экземпляр на время жизни DbContext
            builder.Services.TryAddScoped<IMovieService, MovieService>(); //Один экземпляр на время жизни DbContext
            builder.Services.TryAddTransient<MainPageViewModel>(); //Каждый раз при обращении к MainPageViewModel создается новый экземпляр. Привязка в xaml вызывает дефолт конструктор без параметров, а нам нужны зависимости

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
