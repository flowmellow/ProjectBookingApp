using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Runtime.CompilerServices;

namespace ProjectBookingApp.Views.Controls;


public partial class UserControl : ContentView 
{
    //    public event EventHandler<string> OnError;
    //    public event EventHandler<EventArgs> OnSave;
    //    public event EventHandler<EventArgs> OnCancel;

    //    public UserControl()
    //	{
    //		InitializeComponent();
    //	}

    //    public string FirstName
    //    {
    //        get
    //        {
    //            return entryFirstName.Text;
    //        }
    //        set
    //        {
    //            entryFirstName.Text = value;
    //        }
    //    }

    //    public string LastName
    //    {
    //        get
    //        {
    //            return entryLastName.Text;
    //        }
    //        set
    //        {
    //            entryLastName.Text = value;
    //        }
    //    }

    //    public string Email
    //    {
    //        get
    //        {
    //            return entryEmail.Text;
    //        }
    //        set
    //        {
    //            entryEmail.Text = value;
    //        }
    //    }

    //    public string Password
    //    {
    //        get
    //        {
    //            return entryPassword.Text;
    //        }
    //        set
    //        {
    //            entryPassword.Text = value;
    //        }
    //    }

    //    public string Phone
    //    {
    //        get
    //        {
    //            return entryPhone.Text;
    //        }
    //        set
    //        {
    //            entryPhone.Text = value;
    //        }
    //    }

    //    public string StreetAddress
    //    {
    //        get
    //        {
    //            return entryStreetAddress.Text;
    //        }
    //        set
    //        {
    //            entryStreetAddress.Text = value;
    //        }
    //    }
    //    public string City
    //    {
    //        get
    //        {
    //            return entryCity.Text;
    //        }
    //        set
    //        {
    //            entryCity.Text = value;
    //        }
    //    }
    //    public string State
    //    {
    //        get
    //        {
    //            return entryState.Text;
    //        }
    //        set
    //        {
    //            entryState.Text = value;
    //        }
    //    }
    //    public string ZipCode
    //    {
    //        get
    //        {
    //            return entryZipCode.Text;
    //        }
    //        set
    //        {
    //            entryZipCode.Text = value;
    //        }
    //    }
    //    private void btnSave_Clicked(object sender, EventArgs e)
    //	{
    //        if (firstNameValidator.IsNotValid)
    //        {
    //            OnError?.Invoke(sender, " First Name is required.");
    //            return;
    //        }

    //        if (lastNameValidator.IsNotValid)
    //        {
    //            OnError?.Invoke(sender, " Last Name is required.");
    //            return;
    //        }

    //        if (emailValidator.IsNotValid)
    //        {
    //            foreach (var error in emailValidator.Errors)
    //            {
    //                OnError?.Invoke(sender, error.ToString());
    //            }

    //            return;
    //        }

    //        OnSave?.Invoke(sender, e);
    //    }

    //    private void btnCancel_Clicked(object sender, EventArgs e)
    //    {
    //        OnCancel?.Invoke(sender, e);
    //    }
}
//using CommunityToolkit.Mvvm.ComponentModel;
//using CommunityToolkit.Mvvm.Input;
//using System;
//using System.Threading.Tasks;

//namespace ProjectBookingApp.ViewModels
//{
//    public partial class UserControlViewModel : ObservableObject
//    {
//        [ObservableProperty]
//        private string firstName;

//        [ObservableProperty]
//        private string lastName;

//        [ObservableProperty]
//        private string email;

//        [ObservableProperty]
//        private string password;

//        [ObservableProperty]
//        private string phone;

//        [ObservableProperty]
//        private string streetAddress;

//        [ObservableProperty]
//        private string city;

//        [ObservableProperty]
//        private string state;

//        [ObservableProperty]
//        private string zipCode;

//        public event EventHandler<string> OnError;
//        public event EventHandler<EventArgs> OnSave;
//        public event EventHandler<EventArgs> OnCancel;

//        public UserControlViewModel()
//        {
//            SaveCommand = new AsyncRelayCommand(OnSaveAsync);
//            CancelCommand = new RelayCommand(OnCancel);
//        }

//        public IAsyncRelayCommand SaveCommand { get; }
//        public IRelayCommand CancelCommand { get; }

//        private async Task OnSaveAsync()
//        {
//            if (string.IsNullOrWhiteSpace(FirstName))
//            {
//                OnError?.Invoke(this, "First Name is required.");
//                return;
//            }

//            if (string.IsNullOrWhiteSpace(LastName))
//            {
//                OnError?.Invoke(this, "Last Name is required.");
//                return;
//            }

//            if (string.IsNullOrWhiteSpace(Email))
//            {
//                OnError?.Invoke(this, "Email is required.");
//                return;
//            }

//            // Add more validation as needed

//            OnSave?.Invoke(this, EventArgs.Empty);
//        }

//        private void OnCancel()
//        {
//            OnCancel?.Invoke(this, EventArgs.Empty);
//        }
//    }
//}



