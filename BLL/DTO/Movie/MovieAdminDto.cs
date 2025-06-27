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
    public class MovieAdminDto
    {

        public int Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }
        public string ImageURL { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string TrailerURL { get; set; }
        public Category MovieCategory { get; set; }

        public ICollection<MovieActor> MovieActors { get; set; } = new List<MovieActor>();
    }
}
