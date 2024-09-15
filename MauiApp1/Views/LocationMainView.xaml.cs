using InvetorySystem.ViewModels;
using UseCase;

namespace InventorySystem.Views;

public partial class LocationMainView : ContentPage
{
    private readonly LocationMainViewModel viewModel;
    private readonly IViewDataListUseCase viewDataListUseCase;

    public LocationMainView(LocationMainViewModel locationMainView,IViewDataListUseCase viewDataListUseCase)
	{
		InitializeComponent();
        this.viewModel = locationMainView;
        this.viewDataListUseCase = viewDataListUseCase;
        BindingContext =viewModel;
	}

   
    protected  override void OnAppearing()
            {
            base.OnAppearing();
            this.viewDataListUseCase.ExecuteAsync(string.Empty);       
            }

    public async Task LoadDataAsync()
    {

       this.viewModel.LoadDataAsync();
       

    }
}