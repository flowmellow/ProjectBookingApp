using CommunityToolkit.Mvvm.ComponentModel;
using ProjectBookingApp.Models;
using ProjectBookingApp.Views;
using ProjectBookingApp.Utility;
using ProjectBookingApp.DataManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace ProjectBookingApp.ViewModels
{
    public partial class UpcomingBookingsViewModel : ObservableObject
    {

        private readonly LocalDbService localDbService = new LocalDbService();

        [ObservableProperty]
        private ObservableCollection<Notification> notifications = new ObservableCollection<Notification>();

        [ObservableProperty]
        private ObservableCollection<Appointment> userAppointments = new ObservableCollection<Appointment>();

        [ObservableProperty]
        private ObservableCollection<Appointment> businessAppointments = new ObservableCollection<Appointment>();

        [ObservableProperty]
        public WaitList? userId;

        public UpcomingBookingsViewModel()
        {
            LoadDataAsync();
        }

        private async Task LoadDataAsync()
        {
            var userId = UserSession.Instance.UserId;
            var businessId = UserSession.Instance.BusinessId;

            var userAppointmentsList = await localDbService.GetAppointmentsByUserIdAsync(userId);
            UserAppointments = new ObservableCollection<Appointment>(userAppointmentsList);

            var businessAppointmentsList = await localDbService.GetAppointmentsByBusinessIdAsync(businessId);
            BusinessAppointments = new ObservableCollection<Appointment>(businessAppointmentsList);

            var notificationsList = await localDbService.GetNotificationsAsync();
            Notifications = new ObservableCollection<Notification>(notificationsList);
        }

        [RelayCommand]
        public async Task AcceptNotification(Notification notification)
        {
            try
            {
                var waitListEntry = await localDbService.GetWaitlistEntryByIdAsync(notification.WaitListId);
                if (waitListEntry == null)
                {
                    throw new Exception("Waitlist entry not found.");
                }

                //var appointment = new Appointment
                //{
                //    UserId = UserSession.Instance.UserId,
                //    BusinessId = waitListEntry.BusinessId,
                //    ServiceId = waitListEntry.ServiceId,
                //    AppointmentDate = waitListEntry.WaitListDate,
                //    IsWaitlist = false
                //};

                //await localDbService.CreateAppointmentAsync(appointment);
                await localDbService.DeleteWaitlistEntryAsync(waitListEntry);
                await localDbService.DeleteNotificationAsync(notification);

                Notifications.Remove(notification);
                await LoadDataAsync();

                // Navigate to the services page for the business
                await Shell.Current.GoToAsync(nameof(BusinessAvailablePage), new Dictionary<string, object>
                {
                    { "BusinessId", waitListEntry.BusinessId }
                });
            }
            catch (Exception ex)
            {
                // Handle the exception (e.g., log it, show an alert to the user, etc.)
                await Application.Current.MainPage.DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
            }

        }

        [RelayCommand]
        public async Task DeclineNotification(Notification notification)
        {
            try
            {
                var waitListEntry = await localDbService.GetWaitlistEntryByIdAsync(notification.WaitListId);
                if (waitListEntry != null)
                {
                    await localDbService.DeleteWaitlistEntryAsync(waitListEntry);
                }

                await localDbService.DeleteNotificationAsync(notification);
                Notifications.Remove(notification);
            }
            catch (Exception ex)
            {
                // Handle the exception (e.g., log it, show an alert to the user, etc.)
                await Application.Current.MainPage.DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
            }
        }

        [RelayCommand]
        public async Task CancelAppointment(Appointment appointment)
        {
            bool confirm = await Application.Current?.MainPage?.DisplayAlert("Confirm Cancellation", "Are you sure you want to cancel this appointment?", "Yes", "No");
            if (!confirm)
            {
                return;
            }

            await localDbService.DeleteAppointmentAsync(appointment);

            var waitlistEntries = await localDbService.GetWaitlistEntriesAsync();
            var relevantEntry = waitlistEntries.FirstOrDefault(entry => entry.WaitListDate == appointment.AppointmentDate && entry.BusinessId == appointment.BusinessId);

            if (relevantEntry != null)
            {
                var user = await localDbService.GetUserByWaitlistIdAsync(relevantEntry.WaitListId);
                if (user != null)
                {
                    bool accepted = await NotifyUser(relevantEntry);
                    if (accepted)
                    {
                        await LoadDataAsync();
                        return;
                    }
                }
            }

            await LoadDataAsync();
            //bool confirm = await Application.Current?.MainPage?.DisplayAlert("Confirm Cancellation", "Are you sure you want to cancel this appointment?", "Yes", "No");
            //if (!confirm)
            //{
            //    return;
            //}

            //await localDbService.DeleteAppointmentAsync(appointment);

            //var waitlistEntries = await localDbService.GetWaitlistEntriesAsync();
            //var relevantEntries = waitlistEntries.Where(entry => entry.WaitListDate == appointment.AppointmentDate && entry.BusinessId == appointment.BusinessId).ToList();

            //foreach (var entry in relevantEntries)
            //{
            //    var user = await localDbService.GetUserByWaitlistIdAsync(entry.WaitListId);
            //    if(user != null)
            //    {
            //        NotifyUser(entry);
            //        //var notification = new Notification
            //        //{
            //        //    Message = $"Appointment available on {entry.WaitListDate.ToShortDateString()} at {appointment.BusinessName}",
            //        //    WaitListId = entry.WaitListId,
            //        //    UserId = user.UserId
            //        //};
            //        //await localDbService.CreateNotificationAsync(notification);
            //        //Notifications.Add(notification);
            //    }               

            //    //bool accepted = await NotifyUser(entry);
            //    //if (accepted)
            //    //{
            //    //    break;
            //    //}
            //}

            //await LoadDataAsync();
        }

        public async Task<bool> NotifyUser(WaitList entry)
        {
            
            var business = await localDbService.GetBusinessByIdAsync(entry.BusinessId);
            var user = await localDbService.GetUserByWaitlistIdAsync(entry.WaitListId);
            if (user == null)
            {
                return false;
            }

            var notification = new Notification
            {
                Message = $"Appointment available on {entry.WaitListDate.ToShortDateString()} at {business?.BusinessName ?? "Unknown Business"}",
                WaitListId = entry.WaitListId,
                UserId = user.UserId
            };
            await localDbService.CreateNotificationAsync(notification);
            Notifications.Add(notification);

            bool userResponse = false;
            if (Application.Current?.MainPage != null)
            {
                userResponse = await Application.Current.MainPage.DisplayAlert("Appointment Available", notification.Message, "Accept", "Decline");
            }

            if (userResponse)
            {
                await AcceptNotification(notification);
                await localDbService.DeleteWaitlistEntryAsync(entry);
                await Shell.Current.GoToAsync(nameof(BusinessAvailablePage)); //, new Dictionary<string, object>
                //{
                //    { "BusinessId", entry.BusinessId }
                //});
                return true;
            }
            else
            {
                await DeclineNotification(notification);
                await localDbService.DeleteWaitlistEntryAsync(entry);
                return false;
            }
        }



        [RelayCommand]
        public async Task ReturnToRegistration()
        {
            await Shell.Current.GoToAsync(nameof(RegistrationLandingPage));
        }
    }


}

