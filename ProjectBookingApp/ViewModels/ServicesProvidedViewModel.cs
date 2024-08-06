using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectBookingApp.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using ProjectBookingApp.DataManage;
using ProjectBookingApp.Utility;
using ProjectBookingApp.Views;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using Microsoft.Maui.Controls;

namespace ProjectBookingApp.ViewModels
{
    public partial class ServicesProvidedViewModel : ObservableObject
    {
        private readonly LocalDbService localDbService = new LocalDbService();

        [ObservableProperty]
        ObservableCollection<Services> services = new();

        [ObservableProperty]
        public Business? selectedBusiness;

        //[ObservableProperty]
        //public Business? selectedItem;

        [ObservableProperty]
        public Business? businessName;

        //[ObservableProperty]
        //public Services? typeOfService;

        //[ObservableProperty]
        //public Services? price;

        public ServicesProvidedViewModel()
        {
            //businessName = string.Empty;
            //typeOfService = string.Empty;
            //price = string.Empty;
        }
        public async Task InitializeAsync(Business business)
        {
            SelectedBusiness = business;
            
            await LoadServices();
        }

        [RelayCommand]
        public async Task LoadServices()
        {
            if (SelectedBusiness != null)
            {
                var servicesFromDatabase = await localDbService.GetServicesByBusinessIdAsync(SelectedBusiness.BusinessId);
                Services = new ObservableCollection<Services>(servicesFromDatabase);
                
               
            }
        }

        [RelayCommand]
        public async Task NavigateToAppointmentPage(Services selectedService)
        {
            if (selectedService != null)
            {
                var navigationParameter = new Dictionary<string, object>
                {
                    { "SelectedService", selectedService }
                };
                await Shell.Current.GoToAsync(nameof(AppointmentPage), navigationParameter);
            }
        }
    }

}
