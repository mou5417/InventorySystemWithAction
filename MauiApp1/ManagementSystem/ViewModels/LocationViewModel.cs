using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Entities;
using ManagementSystem.Views;
using ApiService;

namespace ManagementSystem.ViewModels
{
    public partial class LocationViewModel:ObservableObject,IDisposable
    {
      
      
        private string _locationName {  get; set; }
        private string _locationDetail {  get; set; }

        private Guid _itemlocationId { get; set; }
        // private bool initialLoad;
       
        public bool IsNameProvided {  get; set; }
        [ObservableProperty]
        private string vaildateNameMsg;
        private ItemLocation tempData;
        private readonly IServiceGeneric serviceGeneric;

        public ItemLocation TempData
        {
            get
            {
                VaildateNameMsg = "";
                return tempData;
            }

            set {
                //  if (value != null) { VaildateNameMsg = ""; }
               
                SetProperty(ref tempData, value);
                    }
        }
       
        public LocationViewModel(IServiceGeneric serviceGeneric)
        {
           

           TempData = new ItemLocation();
            this.serviceGeneric = serviceGeneric;
        }

        [RelayCommand]
        public async Task Cancel()
        {
            KeepOGData();
            if (App.UserInfo.Role == "1") // Role 1 goes back to R1MainPage
            {
                await Shell.Current.GoToAsync(nameof(R1MainPage));
            }
            else if (App.UserInfo.Role == "2") // Role 2 goes back to R2MainPage
            {
                await Shell.Current.GoToAsync(nameof(R2MainPage));
            }

        }
        private void KeepOGData()
        {
            TempData.LocationName = _locationName;        
            TempData.LocationDetail = _locationDetail;
            OnPropertyChanged(nameof(TempData));
        }

        [RelayCommand]
        public async Task AddLocation()
           {
            
          
            if (VaildateContext())
            {
                
                
               var  AddRequest = DtoFactory.ItemLocationAddRequest(tempData);

                try {
                    await serviceGeneric.CreateDataAsync<ItemLocation, Guid>(AddRequest);
                    ;
                } catch (Exception e)  
                {
                    KeepOGData();
                    VaildateNameMsg = e.Message.ToString();
                    return;
                }

                try
                {
                    // Save data logic here...

                    if (App.UserInfo.Role == "1") // Role 1 goes back to R1MainPage
                    {
                        await Shell.Current.GoToAsync(nameof(R1MainPage));
                    }
                    else if (App.UserInfo.Role == "2") // Role 2 goes back to R2MainPage
                    {
                        await Shell.Current.GoToAsync(nameof(R2MainPage));
                    }

                    // await Shell.Current.GoToAsync("..");
                }
                catch (Exception ex)
                {

                    throw;
                }
                
            }
        }

        public async Task LoadlocationAsync(Guid id)
        {
            if (id != null)
            {
                var itemlocationid = id;
               // var datatype = tempData;
                TempData = await serviceGeneric.GetDataByMatchAsync<ItemLocation,Guid>("itemlocation",itemlocationid);
                tempData.LocationId=id; 
                _locationName = TempData.LocationName;
            }
            
        }

        [RelayCommand]
        internal async Task UpdateLocation()
        {          
            if (VaildateContext()) {
                await serviceGeneric.UpdateDataAsync<ItemLocation, Guid>(tempData, tempData.LocationId);
            }
            await Shell.Current.GoToAsync("..");
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        private bool VaildateContext()
        {
            if (!this.IsNameProvided) 
            {
                KeepOGData();
                this.VaildateNameMsg = "Name can't be empty";
                
                return false;
            }
            return true;
        }
    }
}


