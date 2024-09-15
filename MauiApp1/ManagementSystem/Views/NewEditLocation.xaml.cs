using Entities;
using ManagementSystem.ViewModels;

namespace ManagementSystem.Views;
[QueryProperty(nameof(Locationid), "id")]
public partial class NewEditLocation : ContentPage
{
    private readonly LocationViewModel locationViewModel;
    //private ItemLocationResponse itemLocationResponse;
    private string locationID;
    public string Locationid
    {
        set
        {
            if (value!=null)
            {
                try
                {
                    Guid.TryParse(value,out Guid parsedGuid);

                    LoadLocation(parsedGuid);

                    locationID = value;
                }
                catch (Exception)
                {

                    throw;
                }
                
            }

        }
        get
        {
            return locationID;
        }
    }
    public NewEditLocation( LocationViewModel locationViewModel)
	{
        this.locationViewModel= locationViewModel;
		InitializeComponent();
        this.BindingContext = this.locationViewModel;

    }
    private async Task LoadLocation(Guid id)
    {
       await this.locationViewModel.LoadlocationAsync(id);
    }
}