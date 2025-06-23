using BLL.DTO.Actors;
using BLL.Services.ActorService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO.Movie
{
    public class MoviesbyActorDto
    {

        public ActorDetailsDTO Actor { get; set; }
        public ICollection<MovieDto> Movies { get; set; }
    }
}
