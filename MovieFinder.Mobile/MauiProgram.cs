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
using MovieFinder.Mobile.Views;

namespace MovieFinder.Mobile
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var dbPath = Path.Combine(FileSystem.AppDataDirectory, "movies.db");

            // Синхронно копируем базу данных проекта Data в мобильное хранилище из APK. Синхронность гарантирует, что база скопируется до определения dbContext
            //Не работает для миграций, только для копирования заполненной базы, когда ее нет и продолжения работы с ней
            if (!File.Exists(dbPath))
            {
                using var asset = FileSystem
                    .OpenAppPackageFileAsync("movies.db")     
                    .GetAwaiter().GetResult();                
                using var local = File.Create(dbPath);
                asset.CopyTo(local);                         
            }                                               

            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            
            builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite($"Filename={dbPath}")); //Создает DbContext (главный объект EF core для взаимодействия с бд)                        


            builder.Services.TryAddScoped<IMovieRepository,EfMovieRepository>(); //Один экземпляр на время жизни DbContext
            builder.Services.TryAddScoped<IMovieService, MovieService>(); //Один экземпляр на время жизни DbContext
            builder.Services.TryAddTransient<MainPageViewModel>(); //Каждый раз при обращении к MainPageViewModel создается новый экземпляр. Привязка в xaml вызывает дефолт конструктор без параметров, а нам нужны зависимости
            builder.Services.AddSingleton<MainPage>(); //Создает экземпляр MainPage в конструкторе которого создается MainPageViewModel
#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }

    }
}
