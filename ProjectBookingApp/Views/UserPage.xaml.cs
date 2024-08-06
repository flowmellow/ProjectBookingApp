using ProjectBookingApp.ViewModels;
using System.Windows.Input;

namespace ProjectBookingApp.Views;

public partial class UserPage : ContentPage
{
	UserViewModel vm = new UserViewModel();
	public UserPage()
	{
		InitializeComponent();
		BindingContext = vm;
        LoadUserData();
    }

    private async void LoadUserData()
    {
        await vm.LoadUserData();
    }

}