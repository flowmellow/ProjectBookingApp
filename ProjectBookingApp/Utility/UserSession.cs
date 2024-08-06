using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectBookingApp.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using ProjectBookingApp.ViewModels;
using CommunityToolkit.Mvvm.Input;
using ProjectBookingApp.DataManage;

namespace ProjectBookingApp.Utility
{
    public partial class UserSession : ObservableObject
    {
        private static UserSession instance;
        public static UserSession Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new UserSession();
                }
                return instance;
            }
        }

        [ObservableProperty]
        public bool isUserLoggedIn;

        [ObservableProperty]
        public int userId;


        [ObservableProperty]
        public int businessId;

        [ObservableProperty]
        public string email;

        [ObservableProperty]
        public DateTime lastLoggedIn;


    }

}
