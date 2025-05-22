namespace MovieFinder.Mobile.ViewModels
{
    [QueryProperty(nameof(MovieVm), "movie")]  // 1 атрибут - название свойства в текущем классе, 2 - ключ параметра навигации (ключ в словаре)
    public class MovieDetailsPageViewModel : ViewModelBase
    {
        private MovieViewModel? _movieVm;
        public MovieViewModel MovieVm 
        {
            get => _movieVm;
            set
            {
                if (_movieVm == value) return;
                _movieVm = value;
                OnPropertyChanged(nameof(MovieVm)); 
            }
        }
    }
}
