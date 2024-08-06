using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using System.ComponentModel.DataAnnotations;


namespace ProjectBookingApp.Models
{
    [Table ("user")]
    public class User
    {


        [PrimaryKey, AutoIncrement]
        [Column("user_id")]
        public int UserId { get; set; }

        [Column("business_id")]
        public int BusinessId { get; set; }

        [Column("user_firstname")]
        public string FirstName { get; set; }
        [Column("user_lastname")]
        public string LastName { get; set; }
        [Column("user_email")]
        public string Email { get; set; }
        [Column("user_password")]
        public string Password { get; set; }
        [Column("user_phone")]
        public string? Phone { get; set; }
        [Column("user_streetaddress")]
        public string? StreetAddress { get; set; }
        [Column("user_city")]
        public string? City { get; set; }
        [Column("user_state")]
        public string? State { get; set; }
        [Column("user_zipcode")]
        public string? ZipCode { get; set; }

        
    }
}
