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



        #region Filters

        private bool isFilterVisible;         // Флаг отображения фильтров
        public bool IsFilterVisible          
        {
            get => isFilterVisible;
            set
            {
                isFilterVisible = value;
                OnPropertyChanged(nameof(IsFilterVisible));
            }
        }

        public List<Genre> Genres { get; } = new List<Genre>(Enum.GetValues(typeof(Genre)).Cast<Genre>()); //Список жанров для выбора в фильтре

        private Genre? _selectedGenre;  // Выбранный жанр
        public Genre? SelectedGenre
        {
            get => _selectedGenre;
            set 
            {
                _selectedGenre = value;
                OnPropertyChanged(nameof(SelectedGenre));
            }
        }

        private string _titleFilter = string.Empty; //Название фильма для фильтра
        public string TitleFilter
        {
            get => _titleFilter;
            set
            {
                _titleFilter = value;
                OnPropertyChanged(nameof(TitleFilter));
            }
        }

        private string _actorNameFilter = string.Empty; //Имя актера для фильтра
        public string ActorNameFilter
        {
            get => _actorNameFilter;
            set
            {
                _actorNameFilter = value;
                OnPropertyChanged(nameof(ActorNameFilter));
            }
        }

        [RelayCommand]
        public async Task ResetFilter()  //Сбросить фильтр
        {
            SelectedGenre = null;
            ActorNameFilter = string.Empty;
            TitleFilter = string.Empty;
            await LoadAsync();
        }

        [RelayCommand]
        public async Task ToggleFilterVisibility() //Скрыть фильтр
        {
            IsFilterVisible = !IsFilterVisible;
            if (!IsFilterVisible)
                await ResetFilter();
        }

        #endregion


        public MainPageViewModel(IMovieService movieService)
        {
            _movieService = movieService;
        }

        public async Task LoadAsync() //Загрузить фильмы
        {
            var moviesDto = await _movieService.GetAllWithActorsAsync();
            MoviesToMoviesVM(moviesDto);
        }

        [RelayCommand]
        public async Task SearchMovies() //Найти фильмы по фильтру
        {
            var moviesDto = await _movieService.SearchMoviesAsync(TitleFilter, SelectedGenre, ActorNameFilter);
            MoviesToMoviesVM(moviesDto);
        }

        private void MoviesToMoviesVM(IEnumerable<MovieDto> moviesDto) //Маппинг модели MovieDto в MovieViewModel
        {
            Movies.Clear();
            foreach (var movie in moviesDto)
            {
                Movies.Add(new MovieViewModel(movie, _posterService));
            }
        }
    }
}
