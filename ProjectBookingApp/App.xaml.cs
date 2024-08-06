using ProjectBookingApp.DataManage;
using ProjectBookingApp.Models;

namespace ProjectBookingApp
{
    public partial class App : Application
    {
        public static LocalDbService LocalDbService { get; private set; }
        //public static User CurrentUser { get; private set; }

        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();

            LocalDbService = new LocalDbService();
        }
        

        //public static void SetCurrentUser(User user)
        //{
        //    CurrentUser = user;
        //}
    }
}
