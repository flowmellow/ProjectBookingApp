ProjectBookingApp
ProjectBookingApp is a comprehensive application designed to manage bookings and appointments for businesses and users. 
It leverages the MVVM (Model-View-ViewModel) architecture and utilizes SQLite for data storage. 
The application is built using .NET MAUI and CommunityToolkit.Mvvm for enhanced functionality and maintainability.

Features
•	User Registration and Login: Secure user authentication and registration.
•	Appointment Management: Create, view, and cancel appointments.
•	Business Registration: Register businesses and manage services provided.
•	Notifications: Notify users of available appointments and other updates.
•	Data Persistence: Uses SQLite for local data storage.

Project Structure
The project is organized into several key directories and files:
ViewModels
•	RegistrationLandingViewModel.cs: Handles the logic for the registration landing page.
•	LoginViewModel.cs: Manages user login functionality.
•	AppointmentViewModel.cs: Manages appointment-related operations.
•	UpcomingBookingsViewModel.cs: Handles upcoming bookings and notifications.
•	BusinessRegistrationViewModel.cs: Manages business registration and related operations.
Data Management
•	DataManage: Contains classes and methods for managing data, including database operations.
Models
•	Models: Contains the data models used throughout the application.
Views
•	Views: Contains the XAML pages and their code-behind files for the application's UI.
Utility
•	Utility: Contains utility classes and methods used across the application.
App Configuration
•	App.xaml.cs: Main application configuration file.
•	MauiProgram.cs: Configures services and dependencies for the application.
Getting Started
Prerequisites
•	.NET 6.0 or later
•	Visual Studio 2022 or later with .NET MAUI workload installed

Running the Application
1.	Set the startup project to ProjectBookingApp.
2.	Select the target platform (Android, iOS, Windows, etc.).
3.	Run the application using Visual Studio.
   
Usage
•	Register a User: Navigate to the registration page and fill in the required details.
•	Login: Use your credentials to log in to the application.
•	Manage Appointments: Create, view, and cancel appointments from the appointments page.
•	Register a Business: Navigate to the business registration page to register a new business and manage its services.
•	Receive Notifications: Get notified of available appointments and other updates.

Contributing
Contributions are welcome! Please fork the repository and create a pull request with your changes. Ensure that your code follows the project's coding standards and includes appropriate tests.

License
This project is licensed under the MIT License. See the LICENSE file for details.

Acknowledgements
•	CommunityToolkit.Mvvm: For providing MVVM support.
•	SQLite: For local data storage.
•	.NET MAUI: For building cross-platform applications.

Contact
For any questions or suggestions, please open an issue on GitHub or contact the project maintainers.
