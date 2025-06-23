using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Data.Models.Movie_Module;

namespace BLL.DTO.Movie
{
    public class CreateMovieDTO
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }
        [Required]
        public string ImageURL { get; set; }
        [DataType(DataType.Date)]
        [Required]
        public DateTime ReleaseDate { get; set; }
        public string TrailerURL { get; set; }
        [Required]
        public Category MovieCategory { get; set; }
    }
}
