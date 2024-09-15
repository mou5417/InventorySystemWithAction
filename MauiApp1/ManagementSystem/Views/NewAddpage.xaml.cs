using ManagementSystem.ViewModels;

namespace ManagementSystem.Views;

public partial class NewAddpage : ContentPage
{
    private readonly LocationViewModel locationViewMode;

    public NewAddpage(LocationViewModel locationViewMode)
	{
		InitializeComponent();
        this.locationViewMode = locationViewMode;
        BindingContext=locationViewMode;
    }
}