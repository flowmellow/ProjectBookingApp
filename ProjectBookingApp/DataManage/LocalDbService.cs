using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using ProjectBookingApp.Models;

namespace ProjectBookingApp.DataManage
{
    public class LocalDbService
    {
        private const string DB_BOOKING = "booking_local_db.db3";
        private readonly SQLiteAsyncConnection _connection;

        public LocalDbService()
        {
            string databasePath = Path.Combine(FileSystem.AppDataDirectory, DB_BOOKING);
            _connection = new SQLiteAsyncConnection(databasePath);
            _connection.CreateTableAsync<Services>().Wait();
            _connection.CreateTableAsync<Business>().Wait();
            _connection.CreateTableAsync<User>().Wait();
            _connection.CreateTableAsync<TimeBlock>().Wait();
            _connection.CreateTableAsync<Appointment>().Wait();
            _connection.CreateTableAsync<WaitList>().Wait();
            _connection.CreateTableAsync<Notification>().Wait();
        }

        public async Task<List<User>> GetUsersAsync()
        {
            return await _connection.Table<User>().ToListAsync();
        }

        public async Task<User> LoadUserData()
        {
            return await _connection.Table<User>().FirstOrDefaultAsync();
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _connection.Table<User>().Where(x => x.Email == email).FirstOrDefaultAsync();
        }

        public async Task<User> ValidateUserAsync(string email, string password)
        {
            return await _connection.Table<User>().Where(x => x.Email == email && x.Password == password).FirstOrDefaultAsync();
        }

        public async Task<User> GetById(int id)
        {
            return await _connection.Table<User>().Where(x => x.UserId == id).FirstOrDefaultAsync();
        }

        public async Task Create(User user)
        {
            await _connection.InsertAsync(user);
        }

        public async Task Update(User user)
        {
            await _connection.UpdateAsync(user);
        }

        public async Task Delete(User user)
        {
            await _connection.DeleteAsync(user);
        }

        // **************** Business ****************
        public async Task<List<Business>> GetBusinessAsync()
        {
            return await _connection.Table<Business>().ToListAsync();
        }

        public async Task<Business> GetBusinessById(int id)
        {
            return await _connection.Table<Business>().Where(i => i.BusinessId == id).FirstOrDefaultAsync();
        }
                
        public async Task Create(Business business)
        {
            await _connection.InsertAsync(business);
        }

        public async Task Update(Business business)
        {
            await _connection.UpdateAsync(business);
        }

        public async Task Delete(Business business)
        {
            await _connection.DeleteAsync(business);
        }

        // **************** Services ****************
        public async Task<List<Services>> GetServicesByBusinessIdAsync(int businessId)
        {
            return await _connection.Table<Services>().Where(s => s.BusinessId == businessId).ToListAsync();
        }

        public async Task<Services> GetServiceById(int id)
        {
            return await _connection.Table<Services>().Where(s => s.ServiceId == id).FirstOrDefaultAsync();
        }

        public async Task CreateServiceAsync(Services service)
        {
            await _connection.InsertAsync(service);
        }

        public async Task UpdateServiceAsync(Services service)
        {
            await _connection.UpdateAsync(service);
        }

        public async Task DeleteServiceAsync(Services service)
        {
            await _connection.DeleteAsync(service);
        }

        //****************** TimeBlockInventory ****************

        public async Task<List<TimeBlock>> GetTimeBlocksAsync(int businessId)
        {
            return await _connection.Table<TimeBlock>().Where(tb => tb.BusinessId == businessId).ToListAsync();
        }

        public async Task<TimeBlock> GetTimeBlockById(int id)
        {
            return await _connection.Table<TimeBlock>().Where(tb => tb.Id == id).FirstOrDefaultAsync();
        }

        public async Task CreateTimeBlockAsync(TimeBlock timeBlock)
        {
            await _connection.InsertAsync(timeBlock);
        }

        public async Task UpdateTimeBlockAsync(TimeBlock timeBlock)
        {
            await _connection.UpdateAsync(timeBlock);
        }


        public Task<int> SaveTimeBlockAsync(TimeBlock timeBlock)
        {
            if (timeBlock.Id != 0)
            {
                return _connection.UpdateAsync(timeBlock);
            }
            else
            {
                return _connection.InsertAsync(timeBlock);
            }
        }

        public async Task DeleteTimeBlockByIdAndBusinessIdAsync(int timeBlockId, int businessId)
        {
            var timeBlock = await _connection.Table<TimeBlock>()
                                              .Where(tb => tb.Id == timeBlockId && tb.BusinessId == businessId)
                                              .FirstOrDefaultAsync();
            if (timeBlock != null)
            {
                await _connection.DeleteAsync(timeBlock);
            }
        }

        public Task<int> DeleteTimeBlockAsync(TimeBlock timeBlock)
        {
            return _connection.DeleteAsync(timeBlock);
        }

        //****************** Appointment ****************

        public async Task<List<Appointment>> GetAppointmentsAsync()
        {
            return await _connection.Table<Appointment>().ToListAsync();
        }

        public async Task<List<Appointment>> GetAppointmentsByUserIdAsync(int userId)
        {
            var appointments = await _connection.Table<Appointment>().Where(a => a.UserId == userId).ToListAsync();
            foreach (var appointment in appointments)
            {
                appointment.BusinessName = (await GetBusinessById(appointment.BusinessId))?.BusinessName;
                appointment.ServiceName = (await GetServiceById(appointment.ServiceId))?.TypeOfService;
                appointment.TimeBlock = (await GetTimeBlockById(appointment.TimeBlockId))?.TimeRange;
            }
            return appointments;
        }

        public async Task<List<Appointment>> GetAppointmentsByBusinessIdAsync(int businessId)
        {
            var appointments = await _connection.Table<Appointment>().Where(a => a.BusinessId == businessId).ToListAsync();
            foreach (var appointment in appointments)
            {
                appointment.BusinessName = (await GetBusinessById(appointment.BusinessId))?.BusinessName;
                appointment.ServiceName = (await GetServiceById(appointment.ServiceId))?.TypeOfService;
                appointment.TimeBlock = (await GetTimeBlockById(appointment.TimeBlockId))?.TimeRange;
            }
            return appointments;
        }

        public async Task CreateAppointmentAsync(Appointment appointment)
        {
            await _connection.InsertAsync(appointment);
        }

        public async Task UpdateAppointmentAsync(Appointment appointment)
        {
            await _connection.UpdateAsync(appointment);
        }

        public async Task DeleteAppointmentAsync(Appointment appointment)
        {
            await _connection.DeleteAsync(appointment);
        }
        public async Task DeleteAppointmentByIdAsync(int appointmentId)
        {
            var appointment = await _connection.Table<Appointment>().Where(a => a.AppointmentId == appointmentId).FirstOrDefaultAsync();
            if (appointment != null)
            {
                await _connection.DeleteAsync(appointment);
            }
        }

        public async Task<List<Appointment>> GetAppointmentsByDateAsync(DateTime date)
        {
            return await _connection.Table<Appointment>().Where(a => a.AppointmentDate == date).ToListAsync();
        }

        public async Task<List<Appointment>> GetAppointmentsByDateAndBusinessIdAsync(DateTime date, int businessId)
        {
            return await _connection.Table<Appointment>().Where(a => a.AppointmentDate == date && a.BusinessId == businessId).ToListAsync();
        }

        public async Task<List<Appointment>> GetAppointmentsByDateAndTimeBlockIdAsync(DateTime date, int timeBlockId)
        {
            return await _connection.Table<Appointment>().Where(a => a.AppointmentDate == date && a.TimeBlockId == timeBlockId).ToListAsync();
        }

        // **************** WaitList ****************
        public async Task CreateWaitlistEntryAsync(WaitList waitlist)
        {
            await _connection.InsertAsync(waitlist);
        }

        public async Task<List<WaitList>> GetWaitlistEntriesAsync()
        {
            return await _connection.Table<WaitList>().ToListAsync();
        }

        public async Task<WaitList> GetWaitlistEntryByIdAsync(int waitListId)
        {
            return await _connection.Table<WaitList>().Where(w => w.WaitListId == waitListId).FirstOrDefaultAsync();
        }

        public async Task<User> GetUserByWaitlistIdAsync(int waitListId)
        {
            var waitlistEntry = await _connection.Table<WaitList>().Where(w => w.WaitListId == waitListId).FirstOrDefaultAsync();
            if (waitlistEntry != null)
            {
                return await _connection.Table<User>().Where(u => u.UserId == waitlistEntry.UserId).FirstOrDefaultAsync();
            }
            return null;
        }

        public async Task DeleteWaitlistEntryAsync(WaitList waitlist)
        {
            await _connection.DeleteAsync(waitlist);
        }

        // **************** Notification ****************
        public async Task CreateNotificationAsync(Notification notification)
        {
            await _connection.InsertAsync(notification);
        }

        public async Task<List<Notification>> GetNotificationsAsync()
        {
            return await _connection.Table<Notification>().ToListAsync();
        }

        public async Task DeleteNotificationAsync(Notification notification)
        {
            await _connection.DeleteAsync(notification);
        }
        public async Task<Business> GetBusinessByIdAsync(int id)
        {
            return await _connection.Table<Business>().Where(i => i.BusinessId == id).FirstOrDefaultAsync();
        }


    }


}


