using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Data.Models
{
    public class Ticket
    {
        [Key]
        public int Ticket_Id { get; set; }
        public int RowNumber { get; set; }
        public int SeatNumber { get; set; }
        [ForeignKey("timing")]
        public int Timing_Id { get; set; }
        public Timing? timing { get; set; }
        public bool IsBooked { get; set; } = false;
        public SeatType SeatType { get; set; } 

        [ForeignKey("Order")]
        public int? Order_Id { get; set; }

        public TicketOrder? Order { get; set; }
    }
    public enum SeatType
    {
        Regular,
        Premium
    }

}
