using DAL.Data.Models;
using DAL.Data.Models.Movie_Module;
using DAL.Data.Repositories.GenericRepositories;
using DAL.Data.Repositories.Intrfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Data.Repositories.Calsses
{
    public class MovieRepo : GenericRepo<Movie> ,IMovieRepo
    {
        private readonly AppDbContext _context;
        public MovieRepo(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Movie>> GetMovieByActorAsync(int actorID)
        {
            var moviesWithSpecificActor = await _context.Movies
            .Where(m => m.MovieActors.Any(ma => ma.ActorId == actorID))
            .Include(m => m.MovieActors)
                .ThenInclude(ma => ma.Actor).Select(a =>
                new Movie {
                    Id = a.Id,
                    Name = a.Name,
                    Description = a.Description,
                    ImageURL = a.ImageURL,
                    TrailerURL = a.TrailerURL,
                    MovieCategory= a.MovieCategory,
                  
                })
            .ToListAsync();

            return moviesWithSpecificActor;

        }

        public async Task<IEnumerable<Movie>> GetMoviesByCategoryAsync(Category category)
        {
            var movies = await _context.Movies
                .Where(m => m.MovieCategory == category).ToListAsync();
            return movies;
        }
        public async Task<IEnumerable<Movie>> GetMoviesByProducerAsync(Producer producer)
        {
            var movies = await _context.Movies
                .Where(m => m.ProducerId == producer.Id).ToListAsync();
            return movies;
        }

        public async Task<IEnumerable<Movie>> GetMoviesWithShowtimesAsync()
        {
            var movies = await _context.Movies
                .Include(m => m.Showtimes)
                .ToListAsync();
            return movies;
        }
    }
}
