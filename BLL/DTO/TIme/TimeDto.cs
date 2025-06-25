using DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO.TIme
{
    public class TimeDto
    {
        public int Id { get; set; }
        [DataType(DataType.Time)]
        public TimeSpan StartTime { get; set; }
        public decimal Price { get; set; }
        public ShowTime Showtime { get; set; }
        public ICollection<Ticket> Tickets { get; set; }
    }
}
