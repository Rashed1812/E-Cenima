using DAL.Data.Models;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace DAL.Data
{
    public class ApplicationUser :IdentityUser
    {
        [Display(Name = "Full name")]
        public string FullName { get; set; }
        public ICollection<TicketOrder> TicketOrders { get; set; } = new List<TicketOrder>();
        public virtual ICollection<Review> Reviews { get; set; }
    }
}