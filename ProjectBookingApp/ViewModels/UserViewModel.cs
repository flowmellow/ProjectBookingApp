using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ProjectBookingApp.Models;
using ProjectBookingApp.Views;
using ProjectBookingApp.Utility;
using ProjectBookingApp.DataManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBookingApp.ViewModels
{
       

    
    public partial class UserViewModel : ObservableObject
    {
        //[ObservableProperty]
        //private List<Models.User> users = new List<Models.User>();

        [ObservableProperty]
        private string firstName;

        [ObservableProperty]
        private string lastName;

        [ObservableProperty]
        private string email;

        [ObservableProperty]
        private string password;

        [ObservableProperty]
        private string? phone;

        [ObservableProperty]
        private string? streetAddress;

        [ObservableProperty]
        private string? city;

        [ObservableProperty]
        private string? state;

        [ObservableProperty]
        private string? zipCode;

        private readonly LocalDbService localDbService = new LocalDbService();


        [RelayCommand]
        public async Task SaveUser()
        {
            try
            {
                if (UserSession.Instance.IsUserLoggedIn)
                {
                    throw new Exception("User is already logged in. Use update to save data");
                }
                if (string.IsNullOrEmpty(FirstName))
                {
                    throw new Exception("First Name is required.");
                }

                if (string.IsNullOrEmpty(LastName))
                {
                    throw new Exception("Last Name is required.");
                }

                if (string.IsNullOrEmpty(Email))
                {
                    throw new Exception("Email is required.");
                }
                if (string.IsNullOrEmpty(Password))
                {
                    throw new Exception("Password is required.");
                }

                await AddUser();

                await Application.Current.MainPage.DisplayAlert("Success", "Information saved successfully, Log in", "OK");

                await Shell.Current.GoToAsync(nameof(LoginPage));
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);

                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }

        public async Task AddUser()
        {

            await localDbService.Create(new Models.User
            {
                UserId = 0,
                FirstName = FirstName,
                LastName = LastName,
                Email = Email,
                Password = Password,
                Phone = Phone,
                StreetAddress = StreetAddress,
                City = City,
                State = State,
                ZipCode = ZipCode
            });
        }

        public async Task LoadUserData()
        {
            if (UserSession.Instance.IsUserLoggedIn)
            {
                var user = await localDbService.GetById(UserSession.Instance.UserId);
                if (user != null)
                {
                    FirstName = user.FirstName;
                    LastName = user.LastName;
                    Email = user.Email;
                    Password = user.Password;
                    Phone = user.Phone;
                    StreetAddress = user.StreetAddress;
                    City = user.City;
                    State = user.State;
                    ZipCode = user.ZipCode;
                }
            }
        }


        [RelayCommand]
        public async Task DeleteUser()
        {
            try
            {
                if (UserSession.Instance.IsUserLoggedIn)
                {
                    var user = await localDbService.GetById(UserSession.Instance.UserId);
                    if (user != null)
                    {
                        await localDbService.Delete(user);

                        await Application.Current.MainPage.DisplayAlert("Success", "User deleted successfully", "OK");
                        UserSession.Instance.IsUserLoggedIn = false;
                        await Shell.Current.GoToAsync(nameof(LoginPage));

                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("Error", "User not found", "OK");
                    }
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "User is not logged in", "OK");
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }


        [RelayCommand]
        public async Task CancelAction()
        {
            await Shell.Current.GoToAsync("..");
        }

        [RelayCommand]
        public async Task UpdateUser()
        {
            try
            {
                if (UserSession.Instance.IsUserLoggedIn)
                {
                    var user = await localDbService.GetById(UserSession.Instance.UserId);
                    if (user != null)
                    {
                        user.FirstName = FirstName;
                        user.LastName = LastName;
                        user.Email = Email;
                        user.Password = Password;
                        user.Phone = Phone;
                        user.StreetAddress = StreetAddress;
                        user.City = City;
                        user.State = State;
                        user.ZipCode = ZipCode;

                        await localDbService.Update(user);

                        await Application.Current.MainPage.DisplayAlert("Success", "Information updated successfully", "OK");
                        await Shell.Current.GoToAsync(nameof(RegistrationLandingPage));
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("Error", "User not found", "OK");
                    }
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "User is not logged in", "OK");
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }


    }
}




