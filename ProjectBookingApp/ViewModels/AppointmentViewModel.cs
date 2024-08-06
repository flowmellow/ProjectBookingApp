using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectBookingApp.Views;
using ProjectBookingApp.DataManage;
using ProjectBookingApp.Utility;
using ProjectBookingApp.Models;
using System.Collections.ObjectModel;



namespace ProjectBookingApp.ViewModels
{
    [QueryProperty(nameof(SelectedService), "SelectedService")]
    public partial class AppointmentViewModel : ObservableObject
    {
        private readonly LocalDbService localDbService = new LocalDbService();

        [ObservableProperty]
        private DateTime selectedDate;

        [ObservableProperty]
        private ObservableCollection<TimeBlock> availableTimeBlocks = new ObservableCollection<TimeBlock>();

        [ObservableProperty]
        private ObservableCollection<int> bookedTimeBlockIds = new ObservableCollection<int>();

        [ObservableProperty]
        private bool isWaitlistChecked;

        [ObservableProperty]
        public Business businessName;

        private Services selectedService;
        public Services SelectedService
        {
            get => selectedService;
            set
            {
                SetProperty(ref selectedService, value);
                LoadTimeBlocksAsync().ConfigureAwait(false);
            }
        }


        public AppointmentViewModel()
        {
            SelectedDate = DateTime.Today;
            LoadTimeBlocksAsync();

        }

        public async Task LoadTimeBlocksAsync()
        {
            if (SelectedService != null)
            {
                var timeBlocks = await localDbService.GetTimeBlocksAsync(SelectedService.BusinessId);
                AvailableTimeBlocks.Clear();
                foreach (var timeBlock in timeBlocks)
                {
                    AvailableTimeBlocks.Add(timeBlock);
                }

                var bookedAppointments = await localDbService.GetAppointmentsByDateAndBusinessIdAsync(SelectedDate, SelectedService.BusinessId);
                BookedTimeBlockIds.Clear();
                foreach (var appointment in bookedAppointments)
                {
                    BookedTimeBlockIds.Add(appointment.TimeBlockId);
                }
            }
        }



        [RelayCommand]
        public async Task BookAppointment(TimeBlock timeBlock)
        {
            var mainPage = Application.Current?.MainPage;
            if (mainPage == null) return;

            bool confirm = await Application.Current.MainPage.DisplayAlert(
                "Confirm Appointment",
                $"Do you want to book an appointment on {SelectedDate.ToShortDateString()} from {timeBlock.TimeBlockStart} to {timeBlock.TimeBlockEnd}?",
                "Yes",
                "No");

            if (confirm)
            {
                var appointment = new Appointment
                {
                    UserId = UserSession.Instance.UserId,
                    BusinessId = SelectedService.BusinessId,
                    BusinessName = SelectedService.BusinessName, // Fix: Access the BusinessName property through the Business property
                    ServiceId = SelectedService.ServiceId,
                    TimeBlockId = timeBlock.Id,
                    AppointmentDate = SelectedDate,
                    IsWaitlist = IsWaitlistChecked
                };

                await localDbService.CreateAppointmentAsync(appointment);

                timeBlock.TimeBlockInventory -= 1;
                await localDbService.UpdateTimeBlockAsync(timeBlock);

                await Application.Current.MainPage.DisplayAlert("Success", "Your appointment has been booked.", "OK");
                await LoadTimeBlocksAsync();
            }
        }

        [RelayCommand]
        public async Task SaveWaitlistAsync()
        {
            var mainPage = Application.Current?.MainPage;
            if (mainPage == null) return;

            try
            {
                if (IsWaitlistChecked)
                {
                    var waitlistEntry = new WaitList
                    {
                        BusinessId = SelectedService.BusinessId,
                        BusinessName = SelectedService.BusinessName, // Fix: Access the BusinessName property through the Business property
                        ServiceId = SelectedService.ServiceId,
                        UserId = UserSession.Instance.UserId,
                        WaitListDate = SelectedDate,
                        IsWaitlist = true
                    };

                    await localDbService.CreateWaitlistEntryAsync(waitlistEntry);
                    await mainPage.DisplayAlert("Success", "You have been added to the waitlist.", "OK");
                }
            }
            catch (Exception ex)
            {
                await mainPage.DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
            }
        }

        [RelayCommand]
        public async Task UpcomingBookingsNavigation()
        {
            await Shell.Current.GoToAsync(nameof(UpcomingBookings));

        }
    }
}
