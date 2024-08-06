using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectBookingApp.Models;

namespace ProjectBookingApp.DataManage
{
    public class DataSource
    {
        public static List<User> GetMockData()
        {
            return new List<User>
            {
                new User
                {
                        UserId = 1,
                        FirstName = "John",
                        LastName = "Doe",
                        Email = "johndoe@example.com",
                        Password = "password123",
                        Phone = "1234567890",
                        StreetAddress = "123 Main St",
                        City = "New York",
                        State = "NY",
                        ZipCode = "12345"
                },
                new User
                {
                        UserId = 2,
                        FirstName = "Jane",
                        LastName = "Smith",
                        Email = "janesmith@example.com",
                        Password = "password456",
                        Phone = "9876543210",
                        StreetAddress = "456 Elm St",
                        City = "Los Angeles",
                        State = "CA",
                        ZipCode = "54321"
                }
            };
        }

    }
}
