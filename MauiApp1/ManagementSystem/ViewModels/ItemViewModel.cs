using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Entities;
using ApiService;


namespace ManagementSystem.ViewModels
{

    public partial class ItemViewModel : ObservableObject
    {
        private Item itemDataItem;
        public Item TempDataItem
        {
            get => itemDataItem; set
            {

                SetProperty(ref itemDataItem, value);
            }
        }

        private string ItemName { get; set; }

        private string userId { get; set; }

        public string UserId
        {
            get => userId;
            set
            {
                if (userId != value)
                {
                    userId = value;

                }
            }
        }
        private Guid LocationID { get; set; }
        private string Description { get; set; }
        private int PurchasID { get; set; }
        private User createUser { get; set; }
        public string LocationName { get; set; }

        private readonly IServiceGeneric serviceGeneric;

        public List<string> Status { get; set; }
        //public List<KeyValuePair<int,string>> tempLocationList { get; set; }

        [ObservableProperty]
        private string selectedStatus;

        [ObservableProperty]
        private string selectedLocation;

        [ObservableProperty]
        private List<string> drpLocation;


        private Guid locationId { get; set; } = new();
        private Item itemToAdd { get; set; }
        private Item itemToUpdate { get; set; }
        public List<ItemLocation> TempLocationData { get; set; } = new();

        private ItemLocation location { get; set; }
        public ItemViewModel(IServiceGeneric serviceGeneric)
        {
            this.serviceGeneric = serviceGeneric;
            var testdata = userId;
            LoadUiDataAsync();
            TempDataItem = new();
            Guid userid;
            Guid.TryParse(UserId, out userid);

        }

        private async Task LoadUiDataAsync()
        {
            Status = Enumhelper<ItemStatus>();
            TempLocationData = await LoadDRPdata();
            try
            {
                DrpLocation = TempLocationData.Select(l => l.LocationName).ToList();
            }

            catch (Exception ex)
            {

                var msg = ex.Message;
            }

        }
        public async Task LoadItemDataForEdit(Guid id)
        {
            TempDataItem = await serviceGeneric.GetDataByMatchAsync<Item, Guid>("item", id);
            selectedLocation = TempDataItem.LocationName;
            selectedStatus = TempDataItem.Status;
            Console.WriteLine(TempDataItem);
        }
        private async Task<List<ItemLocation>> LoadDRPdata()
        {

            return await serviceGeneric.GetStore<ItemLocation>(string.Empty);
        }
        private static List<string> Enumhelper<T>()
        {
            return Enum.GetValues(typeof(T)).Cast<T>().Select(e => e.ToString()).ToList();
        }

        [RelayCommand]
        //Should use a better name
        public async Task UpdateItem()
        {
            //item.Status = selectedStatus;
            //TempDataItem.LocationName=TempLocationData.FirstOrDefault()?.LocationName;
            await serviceGeneric.UpdateDataAsync<Item, Guid>(TempDataItem, TempDataItem.ItemId);
            await Shell.Current.GoToAsync("..");
        }
        [RelayCommand]
        public async Task AddItem()
        {

            try
            {
                location = TempLocationData.FirstOrDefault(l => l.LocationName == selectedLocation);
                TempDataItem.ItemId = Guid.NewGuid();
                TempDataItem.Status = selectedStatus;
                // TempDataItem.Id = 7;
                TempDataItem.LocationName = selectedLocation;
                TempDataItem.ItemLocationId = location.LocationId;

                Guid.TryParse(userId, out Guid parsedUserID);
                TempDataItem.CreateUserId = parsedUserID;
                //TempDataItem.CreateUser= await serviceGeneric.GetDataByMatchAsync<User, Guid>("User", parsedUserID);


            }
            catch (Exception ex)
            {

                var test = ex.Message;
            }

            Item itemToAdd = TempDataItem;
            await serviceGeneric.CreateDataAsync<Item, Guid>(itemToAdd);
            await Shell.Current.GoToAsync("..");
        }
        [RelayCommand]
        public void Cancel()
        {
            Shell.Current.GoToAsync("..");
        }


    }
}
