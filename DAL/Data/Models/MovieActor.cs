using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Data.Models.Movie_Module;

namespace DAL.Data.Models
{
    public class MovieActor
    {
        public int MovieId { get; set; }
        public string? RoleName { get; set; }
        public int ActorId { get; set; }
        public Actor Actor { get; set; }
        public Movie Movie { get; set; }
    }
}
