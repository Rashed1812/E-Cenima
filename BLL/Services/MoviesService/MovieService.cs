using BLL.DTO.Movie;
using BLL.Factory.MovieFactory;
using DAL.Data.Models.Movie_Module;
using DAL.Data.Repositories.Intrfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Movies
{
    public class MovieService(IMovieRepo _movieRepo) : IMovieService
    {
        public async Task CreateAsync(CreateMovieDTO createMovieDTO)
        {
            var movie = MovieMapper.MapToCreateMovie(createMovieDTO);
            await _movieRepo.AddAsync(movie);
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<MovieDto>> GetAllAsync()
        {
            var res = await _movieRepo.GetAllAsync();
            return res.Select(s => MovieMapper.MapToDto(s)).ToList();
        }

        public async Task<MovieDetailsDTO> GetByIdAsync(int id)
        {
            var movie = await _movieRepo.FindByIdAsync(id);
            return movie.MapToDetailsDto();
        }

        public Task<ICollection<MovieDto>> GetMoviesByCategoryAsync(int categoryId)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<MovieDto>> GetMoviesWithShowtimesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<MovieDto> UpdateAsync(int id, UpdateMovieDTO updateMovieDTO)
        {
            throw new NotImplementedException();
        }
    }
}
