using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Data.Models
{
    public class ShowTime
    {
        [Key]
        public int Showtime_Id { get; set; }
        public int Cinema_Id { get; set; }
        public int Movie_Id { get; set; }
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        public Cinema Cinema { get; set; }
        public Movie Movie { get; set; }
        public ICollection<Timing> Timings { get; set; }
    }
}
