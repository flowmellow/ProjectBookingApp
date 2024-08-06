using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBookingApp.Models
{
    public class TimeBlock
    {

        [PrimaryKey, AutoIncrement]
        [Column("time_block_id")]
        public int Id { get; set; }

        [Column("business_id")]
        public int BusinessId { get; set; }

        [Column("time_block_start")]
        public TimeSpan TimeBlockStart { get; set; }

        [Column("time_block_end")]
        public TimeSpan TimeBlockEnd { get; set; }

        [Column("time_block_inventory")]
        public int TimeBlockInventory { get; set; }

        [Ignore]        
        public string TimeRange
        {
            get
            {
                return TimeBlockStart.ToString(@"hh\:mm") + " - " + TimeBlockEnd.ToString(@"hh\:mm");
            }
        }

    }
}
