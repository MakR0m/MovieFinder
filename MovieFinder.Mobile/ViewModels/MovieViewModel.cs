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
        

        public MovieViewModel(PosterService posterService, MovieDto movie)
        {
            _posterService = posterService;
            Movie = movie;
        }
    }
}
