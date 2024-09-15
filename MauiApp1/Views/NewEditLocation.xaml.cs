using InvetorySystem.ViewModels;

namespace InventorySystem.Views;
[QueryProperty(nameof(Locationid),"id")]
public partial class NewEditLocation : ContentPage
{
    private readonly  LocationViewModel locationViewModel;
    private int locationID;
    public int Locationid
    {
        set
        {
            if (value!=null)
            {
               
                LoadLocation(value);
                locationID= value;
            }

        }
    }
    public NewEditLocation( LocationViewModel locationViewModel)
	{
        this.locationViewModel= locationViewModel;
		InitializeComponent();
        this.BindingContext = this.locationViewModel;

    }
    private async Task LoadLocation(int locationid)
    {
       await this.locationViewModel.LoadlocationAsync(locationid);
    }
}