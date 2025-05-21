using CommunityToolkit.Mvvm.ComponentModel;
using MovieFinder.Logic.Interfaces;
using MovieFinder.Mobile.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieFinder.Mobile.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private readonly IMovieService _movieService;
        private readonly PosterService _posterService = new PosterService();

        public ObservableCollection<MovieViewModel> Movies { get; } = new();

        public MainPageViewModel(IMovieService movieService)
        {
            _movieService = movieService;
        }

    }
}
