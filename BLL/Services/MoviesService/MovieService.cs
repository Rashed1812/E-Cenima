﻿using BLL.DTO.Cinema;
using BLL.DTO.Movie;
using BLL.Factory.MovieFactory;
using BLL.Services.FileService;
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
    public class MovieService(IMovieRepo _movieRepo,IFileService _fileService) : IMovieService
    {
        public async Task CreateAsync(MovieAddDto createMovieDTO)
        {
            var URL = await _fileService.UploadFileAsync(createMovieDTO.ImageFile, "Images/Movies");

            try
            {
                var movie = new Movie
                {
                    Name = createMovieDTO.Name,
                    Description = createMovieDTO.Description,
                    MovieCategory = createMovieDTO.MovieCategory,
                    TrailerURL = createMovieDTO.TrailerURL,
                    ReleaseDate = createMovieDTO.ReleaseDate,
                    ImageURL = URL,
                    MovieActors = createMovieDTO.Actors?.Select(actorId => new MovieActor { ActorId = actorId.Id }).ToList()
                };
                await _movieRepo.AddAsync(movie);
            }catch(Exception ex)
            {
                 _fileService.DeleteFile(URL);
                throw new Exception(ex.Message);
            }
           
        }

        public async Task<bool> DeleteAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid movie ID.");
            }
            try
            {
                var movie = await _movieRepo.FindByIdAsync(id);
                var result = _movieRepo.Remove(movie);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting movie with ID {id}: {ex.Message}");
            }
        }

        public async Task<ICollection<MovieAdminDto>> GetAllAdminAsync()
        {
            var Movies = await _movieRepo.GetAllMoviesWithActors();
            var res = Movies.Select(x => MovieMapper.MapToAdminDto(x)).ToList();
            return res;
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

        public void UpdateAsync( UpdateMovieDTO updateMovieDTO)
        {
            if (updateMovieDTO == null)
            {
                throw new ArgumentNullException(nameof(updateMovieDTO), "UpdateMovieDTO cannot be null");
            }
            var movie = new Movie
            {
                Id = updateMovieDTO.Id,
                Name = updateMovieDTO.Name,
                Description = updateMovieDTO.Description,
                ImageURL = updateMovieDTO.ImageURL != null ? _fileService.UploadFileAsync(updateMovieDTO.ImageURL, "Images/Movies").Result : null,
                ReleaseDate = updateMovieDTO.ReleaseDate,
                TrailerURL = updateMovieDTO.TrailerURL,
                MovieCategory = updateMovieDTO.MovieCategory,
              
            };
            try
            {
                _movieRepo.Update(movie);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating movie with ID {updateMovieDTO.Id}: {ex.Message}");
            }
        }
    }
}
