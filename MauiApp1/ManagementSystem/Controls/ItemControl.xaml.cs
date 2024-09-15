using System.Runtime.CompilerServices;

namespace ManagementSystem.Controls;

public partial class ItemControl : ContentView
{
	public bool isForAdd { get; set; }
	public bool isForEdit { get; set; }	
	public string Title { get; set; }
	public ItemControl()
	{
        InitializeComponent();
	}

    protected override void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        base.OnPropertyChanged(propertyName);
		if (isForAdd && !isForEdit)
		{
			Title = "新增設備";
			btnSave.SetBinding(Button.CommandProperty, "AddItemCommand");
		}
		else if (!isForAdd && isForEdit)
		{
			Title = "編輯設備";
			btnSave.SetBinding(Button.CommandProperty, "UpdateItemCommand");
		}
	}
}