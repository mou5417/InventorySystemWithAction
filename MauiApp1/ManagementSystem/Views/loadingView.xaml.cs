using ManagementSystem.ViewModels;

namespace ManagementSystem.Views;

public partial class LoadingView : ContentPage
{
	public LoadingView(LoadingViewModel vm)
	{
		BindingContext = vm;
		InitializeComponent();
	}
}