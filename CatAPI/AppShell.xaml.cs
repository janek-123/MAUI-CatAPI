namespace CatAPI
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(CatDetailPage), typeof(CatDetailPage));
        }
    }
}
