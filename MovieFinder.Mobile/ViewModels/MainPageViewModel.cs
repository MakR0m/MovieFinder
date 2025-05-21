using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MovieFinder.Data.Entity;
using MovieFinder.Logic.Interfaces;
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
    public partial class MainPageViewModel : ViewModelBase
    {
        private readonly IMovieService _movieService;
        private readonly PosterService _posterService = new();

        public ObservableCollection<MovieViewModel> Movies { get; } = new();

        public ObservableCollection<Genre> Genres { get; } = new ObservableCollection<Genre>(Enum.GetValues(typeof(Genre)).Cast<Genre>());

        private Genre? _selectedGenre;
        public Genre? SelectedGenre
        {
            get => _selectedGenre;
            set 
            {
                _selectedGenre = value;
                OnPropertyChanged(nameof(SelectedGenre));
            }
        }

        private string _titleFilter = string.Empty;
        public string TitleFilter
        {
            get => _titleFilter;
            set
            {
                _titleFilter = value;
                OnPropertyChanged(nameof(TitleFilter));
            }
        }

        private string _actorNameFilter = string.Empty;
        public string ActorNameFilter
        {
            get => _actorNameFilter;
            set
            {
                _actorNameFilter = value;
                OnPropertyChanged(nameof(ActorNameFilter));
            }
        }
        

        public MainPageViewModel(IMovieService movieService)
        {
            _movieService = movieService;
        }

        public async Task LoadAsync()
        {
            var moviesDto = await _movieService.GetAllWithActorsAsync();
            MoviesToMoviesVM(moviesDto);
        }

        [RelayCommand]
        public async Task SearchMovies()
        {
            var moviesDto = await _movieService.SearchMoviesAsync(TitleFilter, SelectedGenre, ActorNameFilter);
            MoviesToMoviesVM(moviesDto);
        }

        [RelayCommand]
        public async Task ResetFilter()
        {
            SelectedGenre = null;
            ActorNameFilter = string.Empty;
            TitleFilter = string.Empty;
            await LoadAsync();
        }


        private void MoviesToMoviesVM(IEnumerable<MovieDto> moviesDto)
        {
            Movies.Clear();
            foreach (var movie in moviesDto)
            {
                Movies.Add(new MovieViewModel(movie, _posterService));
            }
        }
    }
}
