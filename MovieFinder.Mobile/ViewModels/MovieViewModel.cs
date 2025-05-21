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
        private readonly PosterService _posterService;

        public MovieDto Movie { get;}

        public string Title
        {
            get => Movie.Title;
            set
            {
                Movie.Title = value;
                OnPropertyChanged(nameof(Title));
            }
        }

        public string Description
        {
            get => Movie.Description;
            set
            {
                Movie.Description = value;
                OnPropertyChanged(nameof(Title));
            }
        }

        public List<ActorDto> Actors
        { 
            get => Movie.ActorList;
        }

        public string GenreDisplay => Movie.Genre.GetDescription();
        public Genre Genre 
        { 
            get => Movie.Genre; 
            set 
            { 
                Movie.Genre = value;
                OnPropertyChanged(nameof(Genre));
                OnPropertyChanged(nameof(GenreDisplay));
            } 
        }

        public string ImagePath { get; }
        

        public MovieViewModel(MovieDto movie, PosterService posterService)
        {
            Movie = movie;
            _posterService = posterService;
            ImagePath = _posterService.GetPosterPath(Title);
        }
    }
}
