using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Data.Models
{
    public class Timing
    {
        public int Id { get; set; }
        [DataType(DataType.Time)]
        public TimeSpan StartTime { get; set; }
        public decimal Price { get; set; }
        public int ShowTime_Id { get; set; }
        public ShowTime Showtime { get; set; }
        public ICollection<Ticket> Tickets { get; set; }
    }
}
