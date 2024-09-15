using Entities;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using ManagementSystem.Views;
using ApiService;

namespace ManagementSystem.ViewModels
{
    public partial class LocationMainViewModel : ObservableObject
    {

        
        private bool init = true;

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
        private ObservableCollection<ItemModelForUI> items;
        public ObservableCollection<ItemModelForUI> Items
        {
            get { return items; }
            set
            {
                if (items != value)
                {
                    items = value;
                    OnPropertyChanged(nameof(Items)); // Notify that the collection reference has changed
                }
            }
        }
        private ObservableCollection<User> users;
        public ObservableCollection<User> Users
        {
            get { return users; }
            set
            {
                if (users != value)
                {
                    users = value;
                    OnPropertyChanged(nameof(Users));
                }
            }

        }
        public User user;

        public ItemLocation Itemlocation;

        public Item Item;
        //private Guid userId { get; set; }

        // private readonly IViewDataListUseCase viewDataUseCase;
        private readonly IServiceGeneric ServiceGeneric;

        public LocationMainViewModel(IServiceGeneric serviceApi)
        {
            //  this.viewDataUseCase = viewDataListUseCase;
            ServiceGeneric = serviceApi;

        }


        [RelayCommand]
        public void GotoLocationAdd()
        {
            
            try
            {
                Shell.Current.GoToAsync(nameof(NewAddpage));
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        [RelayCommand]
        public async Task GotoUserAdd()
        {

        }
        [RelayCommand]
        public async Task GotoItemAdd()
        {

            await Shell.Current.GoToAsync(nameof(ItemAddView));
        }
        [RelayCommand]
        public async void GotoItemEdit(Guid id)
        {
            await Shell.Current.GoToAsync($"{nameof(ItemEditView)}?id={id.ToString()}");
            //  Shell.Current.GoToAsync(nameof(ItemEditView));
        }
        [RelayCommand]
        public async Task GotoUserEdit(Guid id)
        {

        }
        [RelayCommand]
        public async void DeleteItemAsync(Guid itemId)
        {
            await ServiceGeneric.RemoveDataAsync<Item, Guid>("Item", itemId);
            await LoadDataAsync();
        }

        [RelayCommand]
        public void Cancel()
        {
            Shell.Current.GoToAsync("..");
        }
        [RelayCommand]
        public async void DeleteLocationAsync(Guid idGuid)
        {
            await ServiceGeneric.RemoveDataAsync<ItemLocation, Guid>("ItemLocation", idGuid);
            await LoadDataAsync();
        }


        [RelayCommand]
        public async Task GotoLocationEdit(Guid idGuid)

        {
            var test = await ServiceGeneric.GetDataByMatchAsync<ItemLocation, Guid>("ItemLocation", idGuid);
            await Shell.Current.GoToAsync($"{nameof(NewEditLocation)}?id={idGuid.ToString()}");
        }

        [RelayCommand]
        public void UpdateLocationAsync()
        {
            ServiceGeneric.UpdateDataAsync<ItemLocation, Guid>(this.Itemlocation, this.Itemlocation.LocationId);
            Shell.Current.GoToAsync("..");
        }


        public async Task LoadDataAsync()
        {

            List<ItemLocation> loadLocationsData = await ServiceGeneric.GetStore<ItemLocation>(string.Empty);
            List<ItemModelForUI> loadItemsData = (await ServiceGeneric.GetStore<Item>(string.Empty))
                .Select(DtoFactory.ItemToItemForUI).ToList();
            List<User> loadUserData = await ServiceGeneric.GetStore<User>(string.Empty);
            ItemLocations = new ObservableCollection<ItemLocation>();
            Items = new ObservableCollection<ItemModelForUI>();
            Users = new ObservableCollection<User>();
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

            if (loadItemsData != null)
            {
                for (int i = 0; i < loadItemsData.Count; i++)
                {

                    Items.Add(loadItemsData[i]);
                }

            }
            if (loadUserData != null)
            {
                for (int i = 0; i < loadUserData.Count; i++)
                {
                    Users.Add(loadUserData[i]);
                }

            }

        }

        private async Task AddDataToList(ItemLocation item)
        {

            ItemLocations.Add(item);
            await Task.Delay(500);
        }


    }
}
