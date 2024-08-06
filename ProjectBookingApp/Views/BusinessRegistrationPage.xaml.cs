using ProjectBookingApp.ViewModels;

namespace ProjectBookingApp.Views;

public partial class BusinessRegistrationPage : ContentPage
{
	BusinessRegistrationViewModel vm = new BusinessRegistrationViewModel();
	public BusinessRegistrationPage()
	{
		InitializeComponent();
		BindingContext = vm;

		LoadBusinessData();
        LoadServices();
        LoadTimeBlocks();
    }

	private async void LoadBusinessData()
    {
        await vm.LoadBusinessData();
    }

    private async void LoadServices()
    {
        await vm.LoadServices();
    }

    private async void LoadTimeBlocks()
    {
        await vm.LoadTimeBlocks();
    }

    //protected override async void OnAppearing()
    //{
    //    base.OnAppearing();
    //    var viewModel = BindingContext as BusinessRegistrationViewModel;
    //    if (viewModel != null)
    //    {
    //        await viewModel.LoadBusinessData();
    //    }
    //}
}