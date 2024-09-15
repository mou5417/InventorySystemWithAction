using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Services;
using Models;

namespace InvetorySystem.ViewModels
{
    public partial class LocationViewModel:ObservableObject,IDisposable
    {
      
        private int _locationID {  get; set; }
        private string _locationName {  get; set; }
        private string _locationDetail {  get; set; }
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
        public void Cancel()
        {
            KeepOGData();
            Shell.Current.GoToAsync("..");

        }
        private void KeepOGData()
        {
            TempData.Name = _locationName;
            TempData.Id = _locationID;
            TempData.LocationDetail = _locationDetail;
            OnPropertyChanged(nameof(TempData));
        }

        [RelayCommand]
        public async Task AddLocation()
        {

           await serviceGeneric.CreateDataAsync<ItemLocation,int>(TempData);
            if (VaildateContext())
            {
                
               var  AddRequest = new ItemLocation
                {
                      Name = TempData.Name,
                      LocationDetail = TempData.LocationDetail,
                     };
                try {

                    ;
                } catch (Exception e)  
                {
                    KeepOGData();
                    VaildateNameMsg = e.Message.ToString();
                    return;
                }
               

                await Shell.Current.GoToAsync("..");
            }
        }

        public async Task LoadlocationAsync(int locationid)
        {
            if (locationid != null)
            {
               // var datatype = tempData;
                TempData = await serviceGeneric.GetDataByMatchAsync<ItemLocation,int>("ItemLocation",locationid);
                _locationID = TempData.Id;
                _locationName = TempData.Name;
            }
        }

        [RelayCommand]
        internal async Task UpdateLocation()
        {
          await  serviceGeneric.UpdateDataAsync<ItemLocation, int>(tempData);
            
            if (VaildateContext()) { 
           await Shell.Current.GoToAsync("..");
            }
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


