using BLL.DTO.Movie;
using DAL.Data.Repositories.Calsses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Movies
{
    public interface IMovieService
    {
          Task<ICollection<MovieDto>> GetAllAsync();
    }
}
