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
        Task<ICollection<MovieAdminDto>> GetAllAdminAsync();
        Task<MovieDetailsDTO> GetByIdAsync(int id);
        Task CreateAsync(MovieAddDto createMovieDTO);
        void UpdateAsync(UpdateMovieDTO updateMovieDTO);
        Task<bool> DeleteAsync(int id);
        Task<ICollection<MovieDto>> GetMoviesByCategoryAsync(int categoryId);
        Task<MovieWithShowTime> GetMoviesWithShowtimesAsync(int MovieID);
        Task<ICollection<MovieDto>> GetMovieByActor(int actorId);
    }
}
