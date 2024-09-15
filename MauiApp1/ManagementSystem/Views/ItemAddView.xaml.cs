using ManagementSystem.ViewModels;

namespace ManagementSystem.Views;

[QueryProperty(nameof(UserId), "UserId")]
public partial class ItemAddView : ContentPage
{
    private readonly ItemViewModel viewModel;

    private string userId;
    public string UserId
    {
        get => userId;

        set
        {
            try
            {
                if (value != null)
                {
                    // bool s=Guid.TryParse(value, out valueParsed);
                    if (userId != value)
                    {
                        userId = value;
                        viewModel.UserId = userId;
                    }
                }
            }
            catch (Exception ex)
            {

                var test = ex.Message;
            }


        }
    }

    public ItemAddView(ItemViewModel viewModel)

    {
        InitializeComponent();
        this.viewModel = viewModel;
        BindingContext = this.viewModel;
    }


    /*  protected override void OnAppearing()
      {
          base.OnAppearing();
          // Ensure the ViewModel's UserId is set after the page appears
          if (viewModel != null)
          {
              viewModel.UserId = userId;
          }
      }*/
}