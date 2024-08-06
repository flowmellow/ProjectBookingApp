using ProjectBookingApp.ViewModels;
using ProjectBookingApp.Models;
using System.Reflection.Emit;

namespace ProjectBookingApp.Views;


public partial class BusinessAvailablePage : ContentPage
{
	
    private readonly BusinessAvailableViewModel vm = new BusinessAvailableViewModel();

    public BusinessAvailablePage()
    {
        InitializeComponent();
        BindingContext = vm;

        // Connect ListView to ViewModel businesses list
        listView.ItemsSource = vm.Businesses;
        //collectionView.ItemsSource = vm.Businesses;

        // Call the method LoadBusinessesFromDatabase() from the ViewModel when the page loads
        LoadBusinesses();
    }
    private async void LoadBusinesses()
    {
        OnAppearing();
        await vm.LoadBusinessesFromDatabase();
    }

    private async void business_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem is Business selectedBusiness)
        {
            var servicesProvidedViewModel = new ServicesProvidedViewModel();
            await servicesProvidedViewModel.InitializeAsync(selectedBusiness);
            await Navigation.PushAsync(new ServicesProvidedPage(servicesProvidedViewModel, selectedBusiness));
        }
    }


}