using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using ProjectBookingApp.DataManage;
using ProjectBookingApp.Models;
using ProjectBookingApp.Views;
using ProjectBookingApp.ViewModels;

namespace ProjectBookingApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddSingleton<LocalDbService>();
            builder.Services.AddTransient<LoginPage>();
            builder.Services.AddTransient<RegistrationLandingPage>();
            builder.Services.AddTransient<UserPage>();
            builder.Services.AddTransient<BusinessRegistrationPage>();           
            builder.Services.AddTransient<BusinessAvailablePage>();
            builder.Services.AddTransient<ServicesProvidedPage>();
            builder.Services.AddTransient<ServicesProvidedViewModel>();
            builder.Services.AddTransient<AppointmentPage>();
            builder.Services.AddTransient<Business>();
            builder.Services.AddTransient<UpcomingBookings>();


#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
