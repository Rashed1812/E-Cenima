using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Data.Models.Movie_Module;

namespace DAL.Data.Models
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

        // Many-to-many relationship with Actor
        public ICollection<MovieActor> MovieActors { get; set; } = new List<MovieActor>();

        // Many-to-one relationship with Showtime
        public ICollection<ShowTime> Showtimes { get; set; }
        public ICollection<Review> Reviews { get; set; }

    }
}
