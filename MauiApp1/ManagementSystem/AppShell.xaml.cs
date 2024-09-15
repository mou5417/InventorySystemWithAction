using ManagementSystem.Views;

namespace ManagementSystem
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(R1MainPage), typeof(R1MainPage));
            Routing.RegisterRoute(nameof(R2MainPage), typeof(R2MainPage));
            Routing.RegisterRoute(nameof(NewAddpage), typeof(NewAddpage));
            Routing.RegisterRoute(nameof(NewEditLocation), typeof(NewEditLocation));
            Routing.RegisterRoute(nameof(ItemAddView), typeof(ItemAddView));
            Routing.RegisterRoute(nameof(ItemEditView), typeof(ItemEditView));
            Routing.RegisterRoute(nameof(LoadingView), typeof(LoadingView));
            Routing.RegisterRoute(nameof(LoginView), typeof(LoginView));
        }
    }
}
