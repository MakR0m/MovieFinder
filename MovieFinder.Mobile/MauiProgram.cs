using Microsoft.Extensions.Logging;
using MovieFinder.Data.Data;
using Microsoft.EntityFrameworkCore;
using System;
using MovieFinder.Logic.Interfaces;
using MovieFinder.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

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
            builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite("Filename=movies.db"));

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
