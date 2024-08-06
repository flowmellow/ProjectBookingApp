using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ProjectBookingApp.DataManage;
using ProjectBookingApp.Models;
using ProjectBookingApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProjectBookingApp.ViewModels
{
    
public partial class BusinessAvailableViewModel : ObservableObject
    {
        [ObservableProperty] public int businessId;
        [ObservableProperty] public string businessName;
        [ObservableProperty] public string? phoneNumber;
        [ObservableProperty] public string businessType;
        [ObservableProperty] public List<DayOfWeek>? daysOfOperation = new();
        [ObservableProperty] public TimeSpan hoursOfOperationStart;
        [ObservableProperty] public TimeSpan hoursOfOperationEnd;
        [ObservableProperty] public bool isMondaySelected;
        [ObservableProperty] public bool isTuesdaySelected;
        [ObservableProperty] public bool isWednesdaySelected;
        [ObservableProperty] public bool isThursdaySelected;
        [ObservableProperty] public bool isFridaySelected;
        [ObservableProperty] public bool isSaturdaySelected;
        [ObservableProperty] public bool isSundaySelected;
        [ObservableProperty] public bool isAvailable;
        [ObservableProperty] public string? availableTime;
        [ObservableProperty] public string? availableDate;
        [ObservableProperty] public string? availableService;

        
        private readonly LocalDbService localDbService = new LocalDbService();

        [ObservableProperty]
        private ObservableCollection<Business> businesses = new();

        public async Task LoadBusinessesFromDatabase()
        {
            var businessesFromDatabase = await FetchBusinessesFromDatabaseAsync();
            Businesses.Clear(); //check if necessary later
            foreach (var business in businessesFromDatabase)
            {
                Businesses.Add(business);
            }
        }

        private async Task<List<Business>> FetchBusinessesFromDatabaseAsync()
        {
            return await localDbService.GetBusinessAsync();
        }

        [RelayCommand]
        public async Task NavigateToServicesProvidedPage(Business selectedBusiness)
        {
            if (selectedBusiness != null)
            {
                await Shell.Current.GoToAsync(nameof(ServicesProvidedPage), true, new Dictionary<string, object>
                {
                    { "SelectedBusiness", selectedBusiness }
                });
            }
        }
    }

}

