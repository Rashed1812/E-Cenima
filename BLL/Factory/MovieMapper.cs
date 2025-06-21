using BLL.DTO.Movie;
using DAL.Data.Models.Movie_Module;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Factory
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

    }
}
