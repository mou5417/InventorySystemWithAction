
using InvetorySystem.ViewModels;
using Services;
using System.Diagnostics;
namespace InventorySystem.Views;

public partial class R1MainPage : ContentPage
{
    private readonly LocationMainViewModel locationMainViewModel;
    private readonly IServiceGeneric serviceGeneric;

    public R1MainPage(LocationMainViewModel locationMainView, IServiceGeneric serviceGeneric)
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

            Debug.Print($"{e.Message}",
                "ok");
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