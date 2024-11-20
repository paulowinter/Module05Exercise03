namespace Module05Exercise01.View;
using Module05Exercise01.ViewModel;
using Module05Exercise01.Services;

public partial class ViewEmployee : ContentPage
{
	public ViewEmployee()
	{
		InitializeComponent();

        var employeeViewModel = new EmployeeViewModel();
        BindingContext = employeeViewModel;
    }

    private async void BackButton(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//MainPage");
    }
}