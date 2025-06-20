using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DAL.Data.Models
{
    public class TicketOrder
    {
        [Key]
        public int Order_Id { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }

        public int Amount { get; set; }

        [DataType(DataType.Currency)]
        [Precision(18, 2)]
        public decimal TotalPrice { get; set; }

        public string? PaymentStatus { get; set; }
        public DateTime BookingTime { get; set; }
        public ApplicationUser User { get; set; }
        public ICollection<Ticket> Tickets { get; set; }
    }
}
