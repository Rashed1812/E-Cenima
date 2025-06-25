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
        Task<MovieDetailsDTO> GetByIdAsync(int id);
        Task CreateAsync(CreateMovieDTO createMovieDTO);
        Task<MovieDto> UpdateAsync(int id, UpdateMovieDTO updateMovieDTO);
        Task<bool> DeleteAsync(int id);
        Task<ICollection<MovieDto>> GetMoviesByCategoryAsync(int categoryId);
        Task<MovieWithShowTime> GetMoviesWithShowtimesAsync(int MovieID);
        Task<ICollection<MovieDto>> GetMovieByActor(int actorId);
    }
}
