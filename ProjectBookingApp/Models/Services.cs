using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace ProjectBookingApp.Models
{
    [Table ("services")]
    public class Services
    {
        [PrimaryKey, AutoIncrement]
        [Column("service_id")]
        public int ServiceId { get; set; }

        [Column("business_id")]
        public int BusinessId { get; set; } // Foreign key to link with Business table

        [Column("business_name")]
        public string BusinessName { get; set; }

        [Column("type_of_service")]
        public string TypeOfService { get; set; }

        [Column("price")]
        public decimal? Price { get; set; }
       
        [Column("appointment_inventory")]
        public int AppointmentInventory { get; set; }

        [Column("time_to_complete")]
        public TimeSpan TimeToCompleteService { get; set; }

        [Column("time_start")]
        public TimeSpan TimeBlockStart { get; set; }

        [Column("time_end")]
        public TimeSpan TimeBlockEnd { get; set; }

       


    }
}
