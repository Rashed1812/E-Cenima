using DAL.Data.Models.Movie_Module;
using DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO.Movie
{
    public class MovieDto
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        [Required] // Mark as required
        public string Description { get; set; }
        [Required] // Mark as required
        public string ImageURL { get; set; }
        [DataType(DataType.Date)]
        [Required]
        public DateTime ReleaseDate { get; set; }
        public string TrailerURL { get; set; }
        [Required]
        public Category MovieCategory { get; set; }
      
       
    }
}
