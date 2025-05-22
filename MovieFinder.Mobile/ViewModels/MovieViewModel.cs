using MovieFinder.Logic.Extensions;
using MovieFinder.Logic.Models;
using MovieFinder.Mobile.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieFinder.Mobile.ViewModels
{
    public class MovieViewModel : ViewModelBase
    {
        public MovieDto Movie { get;} //Модель дто

        public string Title  //Название и прокидка свойства вью модели до свойства дто
        {
            get => Movie.Title;
            set
            {
                Movie.Title = value;
                OnPropertyChanged(nameof(Title));
            }
        }

        public string Description //Описание фильма и прокидка свойства вью модели до свойства дто
        {
            get => Movie.Description;
            set
            {
                Movie.Description = value;
                OnPropertyChanged(nameof(Title));
            }
        }

        public List<ActorDto> Actors //Список актеров
        { 
            get => Movie.ActorList;
        }

        public string GenreDisplay => Movie.Genre.GetDescription(); //Описание жанра на русском языке для отображения
        public Genre Genre //Жанр и прокидка свойства вью модели до свойства дто
        { 
            get => Movie.Genre; 
            set 
            { 
                Movie.Genre = value;
                OnPropertyChanged(nameof(Genre));
                OnPropertyChanged(nameof(GenreDisplay));
            } 
        }

        public string ImagePath { get; } = string.Empty; //Путь к постеру
        
        public double DurationMinutes
        { 
            get => Movie.Duration.TotalMinutes;  //Забираем минуты
            set
            {
                Movie.Duration = TimeSpan.FromMinutes(value); //Отдаем минуты
                OnPropertyChanged(nameof(DurationMinutes));
            }
        }

        public MovieViewModel(MovieDto movie, string path)
        {
            Movie = movie;
            ImagePath = path;
        }
    }
}
