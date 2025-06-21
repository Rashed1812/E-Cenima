using BLL.DTO.Movie;
using BLL.Factory;
using DAL.Data.Models.Movie_Module;
using DAL.Data.Repositories.Calsses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Movies
{
    public class MovieService: IMovieService
    {
        private readonly IMovieRepo _MovieRepo;
        public MovieService(IMovieRepo movieRepo)
        {
            _MovieRepo = movieRepo;
        }

        public async Task<ICollection<MovieDto>> GetAllAsync()
        {

            var res = await _MovieRepo.GetAllAsync();


            return res.Select(s => MovieMapper.MapToDto(s)).ToList();
        }
       

    }
}
