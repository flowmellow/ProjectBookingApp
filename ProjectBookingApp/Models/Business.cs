using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using SQLite;
using CommunityToolkit.Mvvm.ComponentModel;

namespace ProjectBookingApp.Models
{
    [Table("business")]
    public class Business
    {
        [PrimaryKey, AutoIncrement]
        [Column("business_id")]
        public int BusinessId { get; set; }

        [Column("user_id")]
        public int UserId { get; set; } // Foreign key to link with User table

        [Column("business_name")]
        public string BusinessName { get; set; }

        [Column("business_phone_number")]
        public string? PhoneNumber { get; set; }

        [Column("business_type")]
        public string BusinessType { get; set; }

        [Ignore]
        public List<DayOfWeek> DaysOfOperation { get; set; }

        [Column("timeblock_inventory")]
        public int TimeBlockInventory { get; set; }

        [Column("hours_operation_start")]
        public TimeSpan HoursOfOperationStart { get; set; }

        [Column("hours_operation_end")]
        public TimeSpan HoursOfOperationEnd { get; set; }

        [Column("monday_open")]
        public bool IsMondaySelected { get; set; }

        [Column("tuesday_open")]
        public bool IsTuesdaySelected { get; set; }

        [Column("wednesday_open")]
        public bool IsWednesdaySelected { get; set; }

        [Column("thursday_open")]
        public bool IsThursdaySelected { get; set; }

        [Column("friday_open")]
        public bool IsFridaySelected { get; set; }

        [Column("saturday_open")]
        public bool IsSaturdaySelected { get; set; }

        [Column("sunday_open")]
        public bool IsSundaySelected { get; set; }

       
    }
}
