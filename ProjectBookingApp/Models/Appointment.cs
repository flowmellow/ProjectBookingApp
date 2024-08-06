using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace ProjectBookingApp.Models
{
    [Table("appointments")]
    public class Appointment
    {
        [PrimaryKey, AutoIncrement]
        public int AppointmentId { get; set; }

        [Column("business_id")]        
        public int BusinessId { get; set; }

        [Column("service_id")]
        public int ServiceId { get; set; }

        [Column("user_id")]
        public int UserId { get; set; }

        [Column("time_block_id")]
        public int TimeBlockId { get; set; }

        [Column("appointment_date")]
        public DateTime AppointmentDate { get; set; }

        [Column("is_waitlist")]
        public bool IsWaitlist { get; set; }

        [Column("business_name")]
        public string BusinessName { get; set; }

        [Column("service_name")]
        public string ServiceName { get; set; }

        [Column("time_block")]
        public string TimeBlock { get; set; }

    }
}
