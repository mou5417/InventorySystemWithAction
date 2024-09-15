using ManagementSystem.ViewModels;
using ApiService;

namespace ManagementSystem.Views;

public partial class LocationMainView : ContentPage
{
    private readonly LocationMainViewModel viewModel;
    private readonly IServiceGeneric seviceApi;

    public LocationMainView(LocationMainViewModel locationMainView,IServiceGeneric seviceApi)
	{
		InitializeComponent();
        this.viewModel = locationMainView;
        this.seviceApi = seviceApi;
        BindingContext =viewModel;
	}

   
    protected  override async void OnAppearing()
            {
            base.OnAppearing();
             await LoadDataAsync();
            
            }

    public async Task LoadDataAsync()
    {

       this.viewModel.LoadDataAsync();
       

    }
}