using MovieFinder.Logic.Extensions;
using MovieFinder.Logic.Models;

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
                OnPropertyChanged(nameof(Description));
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

        public string ImagePath { get; } = string.Empty;
        
        public double DurationMinutes
        { 
            get => Movie.Duration.TotalMinutes;  
            set
            {
                Movie.Duration = TimeSpan.FromMinutes(value);
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
