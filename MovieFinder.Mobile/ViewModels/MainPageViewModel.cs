using CommunityToolkit.Mvvm.Input;
using MovieFinder.Logic.Interfaces;
using MovieFinder.Logic.Models;
using MovieFinder.Mobile.Services;
using MovieFinder.Mobile.Services.Interfaces;
using MovieFinder.Mobile.Views;
using System.Collections.ObjectModel;

namespace MovieFinder.Mobile.ViewModels
{
    public partial class MainPageViewModel : ViewModelBase
    {
        private readonly IMovieService _movieService;
        private readonly IPosterService _posterService;

        public ObservableCollection<MovieViewModel> Movies { get; } = new();



        #region Filters

        private bool _isFilterVisible;         // Флаг отображения фильтров
        public bool IsFilterVisible          
        {
            get => _isFilterVisible;
            set
            {
                _isFilterVisible = value;
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

        private MovieViewModel? _selectedMovie;
        public MovieViewModel? SelectedMovie
        {
            get => _selectedMovie;
            set 
            {
                if (_selectedMovie == value)       
                    return;
                _selectedMovie = value; 
                OnPropertyChanged();
                if (value != null && OpenDetailsCommand.CanExecute(value))
                    OpenDetailsCommand.ExecuteAsync(value);
            }
        }


        public MainPageViewModel(IMovieService movieService, IPosterService posterService)
        {
            _movieService = movieService;
            _posterService = posterService;
        }

        public async Task LoadAsync() //Загрузить фильмы
        {
            var moviesDto = await _movieService.GetAllWithActorsAsync();
            MoviesToMoviesVM(moviesDto);
        }

        [RelayCommand]
        private async Task SearchMovies() //Найти фильмы по фильтру
        {
            var moviesDto = await _movieService.SearchMoviesAsync(TitleFilter, SelectedGenre, ActorNameFilter);
            MoviesToMoviesVM(moviesDto);
        }

        [RelayCommand]
        private async Task OpenDetailsAsync(MovieViewModel movieVm)
        {
            // один и тот же экземпляр MovieViewModel передаётся в словаре
            var parameters = new Dictionary<string, object>
            {
                ["movie"] = movieVm
            };
            await Shell.Current.GoToAsync(nameof(MovieDetailsPage), parameters); //Переход на страницу информации о фильме с передачей объекта вьюмодели фильма
            SelectedMovie = null; //Сброс выбора
            await ResetFilter(); //Сброс фильтров
        }


        private void MoviesToMoviesVM(IEnumerable<MovieDto> moviesDto) //Маппинг модели MovieDto в MovieViewModel
        {
            Movies.Clear();
            foreach (var dto in moviesDto)
            {
                var path = _posterService.GetPosterPath(dto.Title);
                Movies.Add(new MovieViewModel(dto, path)); //Создание вью модели фильма на основе моделиДто и добавление в список
            }
        }
    }
}
