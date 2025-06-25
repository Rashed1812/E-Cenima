using BLL.DTO.Cinema;
using DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO.Movie
{
    public class MovieWithShowTime
    {
        public MovieDto Movie { get; set; }

        public ICollection<CinmaWithTimes> CinmaWithTimes { get; set; }
    
    }
}
