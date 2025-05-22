using MovieFinder.Mobile.Views;

namespace MovieFinder.Mobile
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(MovieDetailsPage), typeof(MovieDetailsPage)); // Навигация к странице деталей фильма (псевдоним)
        }
    }
}
