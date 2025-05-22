using MovieFinder.Mobile.ViewModels;

namespace MovieFinder.Mobile.Views
{
    public partial class MainPage : ContentPage
    {
        private readonly MainPageViewModel _viewModel;

        public MainPage(MainPageViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            BindingContext = _viewModel;
        }

        protected override async void OnAppearing() //Вызов асинхронного метода после загрузки страницы (вместо того, чтобы синхронно вызывать в конструкторе)
        {
            base.OnAppearing();
            await _viewModel.LoadAsync();
        }
    }

}
