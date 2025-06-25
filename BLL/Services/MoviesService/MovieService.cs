using BLL.DTO.Cinema;
using BLL.DTO.Movie;
using BLL.Factory.MovieFactory;
using DAL.Data.Models;
using DAL.Data.Models.Movie_Module;
using DAL.Data.Repositories.Calsses;
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

        public async Task<ICollection<MovieDto>> GetMovieByActor(int actorId)
        {
            var movies = await _movieRepo.GetMovieByActorAsync(actorId);
           

            return movies.Select(s=>MovieMapper.MapToDto(s)).ToList();
        }

        public Task<ICollection<MovieDto>> GetMoviesByCategoryAsync(int categoryId)
        {
            throw new NotImplementedException();
        }

        public async Task<MovieWithShowTime> GetMoviesWithShowtimesAsync(int MovieId)
        {
            var movies = await _movieRepo.GetMoviesWithShowtimesAsync(MovieId);

            var movieWithShowItems = new MovieWithShowTime
            {
                Movie = new MovieDto
                {
                    Id = movies.Id,
                    Description = movies.Description,
                    ImageURL = movies.ImageURL,
                    Name = movies.Name,
                    MovieCategory = movies.MovieCategory,
                    ReleaseDate = movies.ReleaseDate,
                    TrailerURL = movies.TrailerURL,
                }
                ,
                CinmaWithTimes = movies.Showtimes.Select(s => new CinmaWithTimes
                {
                    Id = s.Cinema.Id,
                    Address = s.Cinema.Address,
                    Capacity = s.Cinema.Capacity,
                    Logo = s.Cinema.Logo,
                    Name = s.Cinema.Name,
                    Date = s.Date,
                    timing = s.Timings
                }).ToList()
            };
            return movieWithShowItems;
        }

        public Task<MovieDto> UpdateAsync(int id, UpdateMovieDTO updateMovieDTO)
        {
            throw new NotImplementedException();
        }
    }
}
