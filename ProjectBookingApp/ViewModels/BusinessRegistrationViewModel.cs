using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectBookingApp.Views;
using ProjectBookingApp.Models;
using ProjectBookingApp.DataManage;
using ProjectBookingApp.Utility;
using System.Collections.ObjectModel;
using System.Windows.Input;


namespace ProjectBookingApp.ViewModels
{

    public partial class BusinessRegistrationViewModel : ObservableObject
    {
        private readonly LocalDbService localDbService = new LocalDbService();

        [ObservableProperty]
        public int businessId;

        [ObservableProperty]
        public int userId;

        [ObservableProperty]
        public string businessName;

        [ObservableProperty]
        public string? phoneNumber;

        [ObservableProperty]
        public string businessType;

        [ObservableProperty]
        public List<DayOfWeek>? daysOfOperation = new();

        [ObservableProperty]
        public TimeSpan hoursOfOperationStart;

        [ObservableProperty]
        public TimeSpan hoursOfOperationEnd;

        [ObservableProperty]
        public bool isMondaySelected;

        [ObservableProperty]
        public bool isTuesdaySelected;

        [ObservableProperty]
        public bool isWednesdaySelected;

        [ObservableProperty]
        public bool isThursdaySelected;

        [ObservableProperty]
        public bool isFridaySelected;

        [ObservableProperty]
        public bool isSaturdaySelected;

        [ObservableProperty]
        public bool isSundaySelected;

        partial void OnIsMondaySelectedChanged(bool value) => UpdateDaysOfOperation(DayOfWeek.Monday, value);
        partial void OnIsTuesdaySelectedChanged(bool value) => UpdateDaysOfOperation(DayOfWeek.Tuesday, value);
        partial void OnIsWednesdaySelectedChanged(bool value) => UpdateDaysOfOperation(DayOfWeek.Wednesday, value);
        partial void OnIsThursdaySelectedChanged(bool value) => UpdateDaysOfOperation(DayOfWeek.Thursday, value);
        partial void OnIsFridaySelectedChanged(bool value) => UpdateDaysOfOperation(DayOfWeek.Friday, value);
        partial void OnIsSaturdaySelectedChanged(bool value) => UpdateDaysOfOperation(DayOfWeek.Saturday, value);
        partial void OnIsSundaySelectedChanged(bool value) => UpdateDaysOfOperation(DayOfWeek.Sunday, value);

        public void UpdateDaysOfOperation(DayOfWeek day, bool isSelected)
        {
            if (isSelected)
            {
                if (!daysOfOperation.Contains(day))
                {
                    daysOfOperation.Add(day);
                }
            }
            else
            {
                if (daysOfOperation.Contains(day))
                {
                    daysOfOperation.Remove(day);
                }
            }
        }
        partial void OnHoursOfOperationEndChanged(TimeSpan value)
        {
            if (value <= HoursOfOperationStart)
            {
                // Handle invalid time range
            }
        }
        public BusinessRegistrationViewModel()
        {
            HoursOfOperationStart = new TimeSpan(9, 0, 0); // 9:00 AM
            HoursOfOperationEnd = new TimeSpan(17, 0, 0);  // 5:00 PM
                                                           //LoadBusinessData().ConfigureAwait(false);
            LoadServices();
        }



        [RelayCommand]
        public async Task SaveBusiness()
        {
            try
            {
                int userId = UserSession.Instance.UserId;
                Business business = new Business
                {
                    BusinessId = 0,
                    BusinessName = BusinessName,
                    PhoneNumber = PhoneNumber,
                    BusinessType = BusinessType,
                    HoursOfOperationStart = HoursOfOperationStart,
                    HoursOfOperationEnd = HoursOfOperationEnd,
                    IsMondaySelected = IsMondaySelected,
                    IsTuesdaySelected = IsTuesdaySelected,
                    IsWednesdaySelected = IsWednesdaySelected,
                    IsThursdaySelected = IsThursdaySelected,
                    IsFridaySelected = IsFridaySelected,
                    IsSaturdaySelected = IsSaturdaySelected,
                    IsSundaySelected = IsSundaySelected,
                    UserId = userId
                };
                await localDbService.Create(business);
                User user = await localDbService.GetById(userId);
                if (user != null)
                {
                    user.BusinessId = business.BusinessId;
                    await localDbService.Update(user);
                    UserSession.Instance.BusinessId = business.BusinessId;

                }

                await Application.Current.MainPage.DisplayAlert("Success", "Information saved successfully", "OK");
                await Shell.Current.GoToAsync(nameof(RegistrationLandingPage));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }

        public async Task LoadBusinessData()
        {

            int businessId = UserSession.Instance.BusinessId;
            var business = await localDbService.GetBusinessById(businessId);
            if (business != null)
            {
                BusinessName = business.BusinessName;
                PhoneNumber = business.PhoneNumber;
                BusinessType = business.BusinessType;
                HoursOfOperationStart = business.HoursOfOperationStart;
                HoursOfOperationEnd = business.HoursOfOperationEnd;
                IsMondaySelected = business.IsMondaySelected;
                IsTuesdaySelected = business.IsTuesdaySelected;
                IsWednesdaySelected = business.IsWednesdaySelected;
                IsThursdaySelected = business.IsThursdaySelected;
                IsFridaySelected = business.IsFridaySelected;
                IsSaturdaySelected = business.IsSaturdaySelected;
                IsSundaySelected = business.IsSundaySelected;
            }

        }



        [RelayCommand]
        public async Task UpdateBusiness()
        {
            try
            {
                int businessId = UserSession.Instance.BusinessId;
                var business = await localDbService.GetBusinessById(businessId);
                if (business != null)
                {
                    business.BusinessName = BusinessName;
                    business.PhoneNumber = PhoneNumber;
                    business.BusinessType = BusinessType;
                    business.HoursOfOperationStart = HoursOfOperationStart;
                    business.HoursOfOperationEnd = HoursOfOperationEnd;
                    business.IsMondaySelected = IsMondaySelected;
                    business.IsTuesdaySelected = IsTuesdaySelected;
                    business.IsWednesdaySelected = IsWednesdaySelected;
                    business.IsThursdaySelected = IsThursdaySelected;
                    business.IsFridaySelected = IsFridaySelected;
                    business.IsSaturdaySelected = IsSaturdaySelected;
                    business.IsSundaySelected = IsSundaySelected;

                    await localDbService.Update(business);

                    await Application.Current.MainPage.DisplayAlert("Success", "Information updated successfully", "OK");
                    await Shell.Current.GoToAsync(nameof(RegistrationLandingPage));
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Business not found", "OK");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }


        [RelayCommand]
        public async Task CancelAction()
        {
            await Shell.Current.GoToAsync(nameof(RegistrationLandingPage));
        }


        [RelayCommand]
        public async Task DeleteBusiness()
        {
            try
            {
                int businessId = UserSession.Instance.BusinessId;
                var business = await localDbService.GetBusinessById(businessId);
                if (business != null)
                {
                    await localDbService.Delete(business);

                    User user = await localDbService.GetById(UserSession.Instance.UserId);
                    if (user != null)
                    {
                        user.BusinessId = 0;
                        await localDbService.Update(user);
                        UserSession.Instance.BusinessId = business.BusinessId;
                    }

                    await Application.Current.MainPage.DisplayAlert("Success", "Business deleted successfully", "OK");
                    await Shell.Current.GoToAsync(nameof(RegistrationLandingPage));
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Business not found", "OK");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }


        [ObservableProperty]
        private ObservableCollection<Services> services = new ObservableCollection<Services>();



        [ObservableProperty]
        private bool isAddServiceFormVisible;

        [ObservableProperty]
        private string typeOfService;

        [ObservableProperty]
        private decimal? price;

        [ObservableProperty]
        private TimeSpan timeToCompleteService;

        [ObservableProperty]
        private bool isEditing;


        [RelayCommand]
        public void ShowAddServiceForm()
        {
            IsAddServiceFormVisible = true;
        }

        public async Task LoadServices()
        {
            var services = await localDbService.GetServicesByBusinessIdAsync(UserSession.Instance.BusinessId);
            Services = new ObservableCollection<Services>(services);
        }



        [RelayCommand]
        public async Task SaveNewService()
        {
            var newService = new Services
            {
                BusinessId = UserSession.Instance.BusinessId,
                TypeOfService = TypeOfService,
                Price = Price,

            };


            if (IsEditing)
            {
                await localDbService.UpdateServiceAsync(newService);
                var existingService = Services.FirstOrDefault(s => s.BusinessId == newService.BusinessId && s.TypeOfService == newService.TypeOfService);
                if (existingService != null)
                {
                    existingService.Price = newService.Price;
                    existingService.TimeToCompleteService = newService.TimeToCompleteService;
                }
                IsEditing = false;
            }
            else
            {
                await localDbService.CreateServiceAsync(newService);
                Services.Add(newService);
            }

            // Clear the form and hide it
            TypeOfService = string.Empty;
            Price = null;
            IsAddServiceFormVisible = false;
        }

        [RelayCommand]
        public void CancelAddService()
        {
            // Clear the form and hide it
            TypeOfService = string.Empty;
            Price = null;
            IsAddServiceFormVisible = false;
        }

        [RelayCommand]
        public async Task EditService(Services service)
        {

            TypeOfService = service.TypeOfService;
            Price = service.Price;

            IsAddServiceFormVisible = true;
            IsEditing = true;
            await Task.CompletedTask; // Added await statement
        }

        [RelayCommand]
        public async Task DeleteService(Services service)
        {
            await localDbService.DeleteServiceAsync(service);
            Services.Remove(service);
        }

        [ObservableProperty]
        private ObservableCollection<TimeBlock> timeBlocks = new ObservableCollection<TimeBlock>();


        [ObservableProperty]
        public TimeSpan timeBlockStart;

        [ObservableProperty]
        public TimeSpan timeBlockEnd;

        [ObservableProperty]
        public int timeBlockInventory;

        //public IEnumerable<TimeBlock> SortedTimeBlocks => TimeBlocks.OrderBy(tb => tb.TimeBlockStart);

        public async Task LoadTimeBlocks()
        {
            var businessId = UserSession.Instance.BusinessId;
            var timeBlocksList = await localDbService.GetTimeBlocksAsync(businessId);
            TimeBlocks.Clear();
            foreach (var timeBlock in timeBlocksList)
            {

                TimeBlocks.Add(timeBlock);
            }

        }


        [RelayCommand]
        public async Task AddTimeBlock()
        {
            var newTimeBlock = new TimeBlock
            {
                BusinessId = UserSession.Instance.BusinessId,
                TimeBlockStart = TimeBlockStart,
                TimeBlockEnd = TimeBlockEnd

            };

            await localDbService.CreateTimeBlockAsync(newTimeBlock);
            TimeBlocks.Add(newTimeBlock);
            IncrementTimeBlockInventory(newTimeBlock);

        }


        [RelayCommand]
        public async Task DeleteTimeBlock(TimeBlock timeBlock)
        {
            try
            {
                if (timeBlock == null) return;

                int businessId = UserSession.Instance.BusinessId;
                await localDbService.DeleteTimeBlockByIdAndBusinessIdAsync(timeBlock.Id, businessId);

                TimeBlocks.Remove(timeBlock);
                await DecrementTimeBlockInventory(timeBlock); // Decrement the inventory

                await Application.Current.MainPage.DisplayAlert("Success", "TimeBlock deleted successfully", "OK");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }

        async Task DecrementTimeBlockInventory(TimeBlock timeBlock)
        {
            try
            {
                if (timeBlock == null) return;

                int businessId = UserSession.Instance.BusinessId;
                timeBlock.TimeBlockInventory--;
                await localDbService.UpdateTimeBlockAsync(timeBlock);

                await Application.Current.MainPage.DisplayAlert("Success", "TimeBlock inventory decremented successfully", "OK");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }
        async Task IncrementTimeBlockInventory(TimeBlock timeBlock)
        {
            try
            {
                if (timeBlock == null) return;

                int businessId = UserSession.Instance.BusinessId;
                timeBlock.TimeBlockInventory++;
                await localDbService.UpdateTimeBlockAsync(timeBlock);

                await Application.Current.MainPage.DisplayAlert("Success", "TimeBlock inventory incremented successfully", "OK");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }

    }


}







//public ObservableCollection<string> BusinessTypes { get; } = new ObservableCollection<string>
//{
//    "Restaurant",
//    "Salon",
//    "Gym",
//    "Retail",
//    "Other"
//};

//[RelayCommand]
//public async void CancelAction()
//{
//    await Shell.Current.GoToAsync("..");
//}


