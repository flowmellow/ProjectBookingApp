using ProjectBookingApp.ViewModels;
using ProjectBookingApp.Models;
namespace ProjectBookingApp.Views;

public partial class ServicesProvidedPage : ContentPage
{
    private readonly ServicesProvidedViewModel vm;
    private readonly Business selectedBusiness;

    public ServicesProvidedPage(ServicesProvidedViewModel viewModel, Business business)
    {
        vm = viewModel;
        selectedBusiness = business;

        InitializeComponent();

        BindingContext = vm;

        vm.InitializeAsync(selectedBusiness);

        OnAppearing(); 
    }


}
