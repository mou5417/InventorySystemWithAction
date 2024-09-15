using System.Runtime.CompilerServices;

namespace InventorySystem.Controls;

public partial class LocationControl : ContentView
{
	public bool isForAdd { get; set; }
	public bool isForEdit { get; set; }
	
	
	public LocationControl()
	{
		InitializeComponent();
	}

    protected override void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        base.OnPropertyChanged(propertyName);
		if (isForAdd && !isForEdit)
		{
			btnSave.SetBinding(Button.CommandProperty, "AddLocationCommand");
		}

		else if (!isForAdd && isForEdit)
		{
            btnSave.SetBinding(Button.CommandProperty, "UpdateLocationCommand");
        }	
    }
}