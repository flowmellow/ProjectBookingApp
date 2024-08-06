using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectBookingApp.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ProjectBookingApp.Views;
using ProjectBookingApp.Utility;
using ProjectBookingApp.DataManage;

namespace ProjectBookingApp.ViewModels
{

    public partial class RegistrationLandingViewModel : ObservableObject
    {
       


        [RelayCommand]
        public async Task UserRegistrationNavigation()
        {

            await Shell.Current.GoToAsync(nameof(UserPage));         



        }

        [RelayCommand]
        public async Task BusinessRegistrationNavigation()
        {
            if (UserSession.Instance.IsUserLoggedIn)
            {
                await Shell.Current.GoToAsync(nameof(BusinessRegistrationPage));
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Not logged in", "Click here to login");
                await Shell.Current.GoToAsync(nameof(LoginPage));
            }
        }

        [RelayCommand]
        public async Task BusinessAvailable()
        {
            if (UserSession.Instance.IsUserLoggedIn)
            {
                await Shell.Current.GoToAsync(nameof(BusinessAvailablePage));
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Not logged in", "Click here to login");
                await Shell.Current.GoToAsync(nameof(LoginPage));
            }
        }

        [RelayCommand]
        public async Task UpcomingBookingsNavigation()
        {
            if (UserSession.Instance.IsUserLoggedIn)
            {
                await Shell.Current.GoToAsync(nameof(UpcomingBookings));
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Not logged in", "Click here to login");
                await Shell.Current.GoToAsync(nameof(LoginPage));
            }
        }

        [RelayCommand]
        public async Task SignOut()
        {
            // Perform sign-out logic
            UserSession.Instance.IsUserLoggedIn = false;
            UserSession.Instance.UserId = 0;
            UserSession.Instance.BusinessId = 0;

            // Navigate to the login page
            await Shell.Current.GoToAsync(nameof(LoginPage));
        }
    }
}






