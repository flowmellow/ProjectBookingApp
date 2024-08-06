using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ProjectBookingApp.Models;
using ProjectBookingApp.ViewModels;

namespace ProjectBookingApp.Views;


public partial class RegistrationLandingPage : ContentPage
{
	RegistrationLandingViewModel vm = new RegistrationLandingViewModel();
	public RegistrationLandingPage()
	{
		InitializeComponent();
		BindingContext = vm;
	}
}