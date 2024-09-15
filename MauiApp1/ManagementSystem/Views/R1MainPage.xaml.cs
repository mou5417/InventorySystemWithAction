using ManagementSystem.ViewModels;
using ApiService;
using System.Diagnostics;
using Serilog.Core;
using Serilog;
using Microsoft.Extensions.Logging;
namespace ManagementSystem.Views;

public partial class R1MainPage : ContentPage
{
    private readonly LocationMainViewModel locationMainViewModel;
    private readonly IServiceGeneric serviceGeneric;
    private readonly ILogger<R1MainPage> _logger;
    public R1MainPage(LocationMainViewModel locationMainView, IServiceGeneric serviceGeneric, ILogger<R1MainPage> logger)
    {

        InitializeComponent();
        this.locationMainViewModel = locationMainView;
        this.serviceGeneric = serviceGeneric;
        BindingContext = locationMainView;
        this.locationMainViewModel = locationMainView;
        _logger = logger;
        logger.LogInformation("R1MainPage is loading");
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