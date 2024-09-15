using ManagementSystem.ViewModels;

namespace ManagementSystem.Views;

public partial class LoginView : ContentPage
{
    private readonly LoginViewModel vm;

    public LoginView(LoginViewModel viewmodel)
	{
		InitializeComponent();
        vm = viewmodel;
        BindingContext = vm;
    }
}