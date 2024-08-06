using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBookingApp.Models
{
    [Table("notification")]
    public class Notification
    {

        [PrimaryKey, AutoIncrement]
        public int NotificationId { get; set; }

        [Column("message")]
        public string? Message { get; set; }

        [Column("waitlist_id")]
        public int WaitListId { get; set; }

        [Column("user_id")]
        public int UserId { get; set; }
    }
}
