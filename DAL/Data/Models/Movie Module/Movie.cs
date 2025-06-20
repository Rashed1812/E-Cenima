using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Data.Models.Movie_Module
{
    public class Movie
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
        [Required]
        public int ProducerId { get; set; }
        [ForeignKey("ProducerId")]
        public Producer Producer { get; set; }
        public ICollection<MovieActor> MovieActors { get; set; } = new List<MovieActor>();
        public ICollection<ShowTime> Showtimes { get; set; }
        public ICollection<Review> Reviews { get; set; }
    }
}
