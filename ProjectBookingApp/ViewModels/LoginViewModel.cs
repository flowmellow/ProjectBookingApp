using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectBookingApp.Views;
using ProjectBookingApp.Models;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;
using ProjectBookingApp.DataManage;
using ProjectBookingApp.Utility;


namespace ProjectBookingApp.ViewModels
{
    [ObservableObject]
    public partial class LoginViewModel
    {
        [ObservableProperty]
        public string email;

        [ObservableProperty]
        public string password;

        LocalDbService localDbService = new LocalDbService();
        UserSession userSession = UserSession.Instance;
            

        [RelayCommand]
        public async Task LogIn()
        {
            User user = null;
            if (Email == "admin" && Password == "admin")
            {
                user = new User { UserId = 1, Email = "admin" }; // Mock admin user
            }
            else
            {
                user = await localDbService.ValidateUserAsync(Email, Password);
            }

            if (user != null)
            {
                userSession.UserId = user.UserId;
                userSession.Email = user.Email;
                userSession.BusinessId = user.BusinessId;
                userSession.IsUserLoggedIn = true;
                userSession.LastLoggedIn = DateTime.Now;

                await Shell.Current.GoToAsync(nameof(RegistrationLandingPage));
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Invalid email or password", "OK");
            }
           
        }
      

        [RelayCommand]
        public async Task UserRegistration()
        {
            await Shell.Current.GoToAsync(nameof(RegistrationLandingPage));
        }
    }
}
