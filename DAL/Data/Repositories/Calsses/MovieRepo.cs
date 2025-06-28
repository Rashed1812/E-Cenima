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

        public async Task<IEnumerable<Movie>> GetAllMoviesWithActors()
        {
            return  await _context.Movies.Include(m => m.MovieActors).ThenInclude(m => m.Actor).ToListAsync();

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

      

        public async Task<Movie> GetMoviesWithShowtimesAsync(int MovieId)
        {
            var movie = await _context.Movies.Include(m => m.Showtimes)
                 .ThenInclude(sh => sh.Cinema).ThenInclude(sh => sh.Showtimes).ThenInclude(c=>c.Timings).Where(m=>m.Id==MovieId).FirstOrDefaultAsync();
            return movie!;
        }
    }
}
