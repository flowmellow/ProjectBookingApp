using CommunityToolkit.Mvvm.ComponentModel;
using ProjectBookingApp.ViewModels;
namespace ProjectBookingApp.Views;

public partial class AppointmentPage : ContentPage
{
	private readonly AppointmentViewModel vm = new AppointmentViewModel();
	public AppointmentPage()
	{
		InitializeComponent();

		BindingContext = vm;

      
    }

}

