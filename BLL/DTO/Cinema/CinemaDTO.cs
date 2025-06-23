using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Data.Models;

namespace BLL.DTO.Cinema
{
    public class CinemaDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }
        public string? Logo { get; set; }
        public ICollection<ShowTime> Showtimes { get; set; }
    }
}
