using ManagementSystem.ViewModels;

namespace ManagementSystem.Views;

[QueryProperty(nameof(ItemId),"id")]
public partial class ItemEditView : ContentPage
{
    private readonly ItemViewModel viewModel;

    private string itemId { get; set; }
	public string ItemId { get => itemId; set 
		{
			if (value != null)
			{
				Guid.TryParse(value, out Guid parsedId);
				LoadDataAsync(parsedId);
				itemId = value;
			}
		
		} }

	public ItemEditView(ItemViewModel viewModel)
	{
        this.viewModel = viewModel;
        BindingContext = viewModel;
        InitializeComponent();

    }

	private async Task LoadDataAsync(Guid id)
	{
		if (id != null)
		{
			try
			{
                await this.viewModel.LoadItemDataForEdit(id);
            }
			catch (Exception ex)
			{

				Console.WriteLine($"{ex.Message}");
			} 
		
		}
		
	}
}