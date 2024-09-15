using ManagementSystem.ViewModels;
using ApiService;
namespace ManagementSystem.Views;

public partial class R2MainPage : ContentPage
{
    private readonly LocationMainViewModel locationMainViewModel;
    private readonly IServiceGeneric serviceGeneric;

    public R2MainPage(LocationMainViewModel locationMainView, IServiceGeneric serviceGeneric)
	{
		InitializeComponent();
        this.locationMainViewModel = locationMainView;
        this.serviceGeneric = serviceGeneric;
        BindingContext = locationMainView;
        this.locationMainViewModel = locationMainView;
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        try
        {
            await LoadData();
        }
        catch (Exception e)
        {

            Console.WriteLine(e.Message);
                
        }
        
        
    }
    private async Task LoadData()
    {
       await locationMainViewModel.LoadDataAsync();
        
    }
    private void OnCounterClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync(nameof(LocationMainView));
    }
}