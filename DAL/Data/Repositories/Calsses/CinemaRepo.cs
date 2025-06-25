using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Data.Models;
using DAL.Data.Repositories.GenericRepositories;
using DAL.Data.Repositories.Intrfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Data.Repositories.Calsses
{
    public class CinemaRepo: GenericRepo<Cinema>, ICinemaRepo
    {
        private readonly AppDbContext _context;

        public CinemaRepo(AppDbContext context) : base(context)
        {
            this._context = context;
        }

        public async Task<ICollection<Cinema>> GetAllWiithTime()
        {
           return await  _context.Cinemas.Include(c => c.Showtimes)
                .ThenInclude(c=>c.Movie).ToListAsync();
            
        }
    }
}
