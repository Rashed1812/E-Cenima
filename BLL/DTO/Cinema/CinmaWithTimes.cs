using DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO.Cinema
{
    public class CinmaWithTimes
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }
        public int Capacity { get; set; }
        public string? Logo { get; set; }
        public DateTime Date { get; set; }

        public ICollection<Timing> timing { get; set; }
    }
}
