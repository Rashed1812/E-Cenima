using DAL.Data.Models;
using DAL.Data.Models.Movie_Module;
using DAL.Data.Repositories.GenericRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Data.Repositories.Intrfaces
{
    public interface IMovieRepo:IGenericRepo<Movie>
    {
        Task<IEnumerable<Movie>> GetMoviesByCategoryAsync(Category category);
        Task<Movie> GetMoviesWithShowtimesAsync(int MovieID);
        Task<IEnumerable<Movie>> GetMovieByActorAsync(int actorID);
        Task<IEnumerable<Movie>> GetAllMoviesWithActors();
    }
}
