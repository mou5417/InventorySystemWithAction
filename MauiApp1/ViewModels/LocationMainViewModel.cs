using Models;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using UseCase;
using Services;
using InventorySystem.Views;
using CommunityToolkit.Mvvm.Input;
namespace InvetorySystem.ViewModels;

public partial class LocationMainViewModel : ObservableObject
{
    //public string UId { get => itemlocation.Id.ToString(); }




    private ObservableCollection<ItemLocation> itemLocations;
    public ObservableCollection<ItemLocation> ItemLocations
    {
        get { return itemLocations; }
        set
        {
            if (itemLocations != value)
            {
                itemLocations = value;
                OnPropertyChanged(nameof(ItemLocations)); // Notify that the collection reference has changed
            }
        }
    }

    [ObservableProperty]
    private ItemLocation itemlocation;

    private readonly IViewDataListUseCase viewDataUseCase;
    private readonly IServiceGeneric ServiceGeneric;

    public LocationMainViewModel(IViewDataListUseCase viewDataListUseCase, IServiceGeneric serviceGeneric)
    {
        this.viewDataUseCase = viewDataListUseCase;
        ServiceGeneric = serviceGeneric;
    }


    [RelayCommand]
    public void GotoAdd()
    {
        Shell.Current.GoToAsync(nameof(NewAddpage));

    }

    [RelayCommand]
    public void Cancel()
    {
        Shell.Current.GoToAsync("..");
    }
    [RelayCommand]
    public async void DeleteAsync(int locationid)
    {

        await ServiceGeneric.RemoveDataAsync<ItemLocation, int>("ItemLocation", locationid);
        await LoadDataAsync();
    }

    [RelayCommand]
    public void GOtoEdit(int locationid)
    {
        Shell.Current.GoToAsync($"{nameof(NewEditLocation)}?id={locationid}");
    }

    [RelayCommand]
    public void UpdateLocationAsync()
    {
        ServiceGeneric.UpdateDataAsync<ItemLocation, int>(this.Itemlocation);
        Shell.Current.GoToAsync("..");
    }


    public async Task LoadDataAsync()
    {

        List<ItemLocation> loadLocationsData = await ServiceGeneric.GetStore<ItemLocation>();
        ItemLocations = new ObservableCollection<ItemLocation>();
        var tasks = new List<Task>();
        if (loadLocationsData != null)
        {
            for (int i = 0; i < loadLocationsData.Count; i++)
            {

                ItemLocation location = loadLocationsData[i];
                tasks.Add(AddDataToList(location));
            }

            await Task.WhenAll(tasks);

        }


    }

    private async Task AddDataToList(ItemLocation item)
    {

        ItemLocations.Add(item);
        await Task.Delay(500);
    }


}
