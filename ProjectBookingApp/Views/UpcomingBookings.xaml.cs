using CommunityToolkit.Mvvm.ComponentModel;
using ProjectBookingApp.ViewModels;

namespace ProjectBookingApp.Views;


public partial class UpcomingBookings : ContentPage
{
	private readonly UpcomingBookingsViewModel vm = new UpcomingBookingsViewModel();
	public UpcomingBookings()
	{
		InitializeComponent();
		BindingContext = vm;
	}
}