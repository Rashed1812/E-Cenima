using DAL.Data.Models.Movie_Module;
using DAL.Data.Repositories.GenericRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Data.Repositories.Calsses
{
    public class MovieRepo : GenericRepo<Movie> ,IMovieRepo
    {
        public MovieRepo(AppDbContext context) : base(context)
        {
        }


    }
}
