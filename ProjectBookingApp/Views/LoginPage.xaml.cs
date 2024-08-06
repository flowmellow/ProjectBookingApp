using Microsoft.Maui.ApplicationModel.Communication;
using ProjectBookingApp.ViewModels;

namespace ProjectBookingApp.Views

{
    public partial class LoginPage : ContentPage
    {
        private readonly LoginViewModel vm = new LoginViewModel();
        public LoginPage()
        {
            InitializeComponent();
            BindingContext = vm;
        }

        
    }
    

}
