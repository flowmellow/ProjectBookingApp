using ProjectBookingApp.Views;

namespace ProjectBookingApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            //var getUserSavedKey = Preferences.Get("UserAlreadyLoggedIn", false);

            //if (getUserSavedKey == true)
            //{
            //    GoToAsync(nameof(RegistrationLandingPage));
            //}
            //else
            //{
            //    GoToAsync(nameof(LoginPage));
            //}

            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
            Routing.RegisterRoute(nameof(RegistrationLandingPage), typeof(RegistrationLandingPage));
            Routing.RegisterRoute(nameof(UserPage), typeof(UserPage));            
            Routing.RegisterRoute(nameof(BusinessRegistrationPage), typeof(BusinessRegistrationPage));            
            Routing.RegisterRoute(nameof(BusinessAvailablePage), typeof(BusinessAvailablePage));
            Routing.RegisterRoute(nameof(ServicesProvidedPage), typeof(ServicesProvidedPage));
            Routing.RegisterRoute(nameof(AppointmentPage), typeof(AppointmentPage));
            Routing.RegisterRoute(nameof(UpcomingBookings), typeof(UpcomingBookings));
        }
    }
}
