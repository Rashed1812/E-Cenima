using BLL.DTO.Movie;
using DAL.Data.Models.Movie_Module;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Factory.MovieFactory
{
    public static class MovieMapper
    {
        public static MovieDto MapToDto(Movie movie)
        {   
            if (movie == null) return null;

            return new MovieDto
            {
                Id = movie.Id,
                Name = movie.Name,
                Description = movie.Description,
                ImageURL = movie.ImageURL,
                MovieCategory = movie.MovieCategory,
                ReleaseDate = movie.ReleaseDate,
                TrailerURL = movie.TrailerURL,
            };
        }
        public static MovieDetailsDTO MapToDetailsDto(this Movie movie)
        {
            if (movie == null) return null;
            return new MovieDetailsDTO()
            {
                Id = movie.Id,
                Name = movie.Name,
                Description = movie.Description,
                ImageURL = movie.ImageURL,
                MovieCategory = movie.MovieCategory,
                ReleaseDate = movie.ReleaseDate,
                TrailerURL = movie.TrailerURL,
            };
        }
        public static Movie MapToCreateMovie(CreateMovieDTO createMovieDto)
        {
            if (createMovieDto == null) return null;
            return new Movie
            {
                Name = createMovieDto.Name,
                Description = createMovieDto.Description,
                ImageURL = createMovieDto.ImageURL,
                MovieCategory = createMovieDto.MovieCategory,
                ReleaseDate = createMovieDto.ReleaseDate,
                TrailerURL = createMovieDto.TrailerURL,
            };
        }

    }
}
