using DAL.Data.Models;
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
    public class TimeRepo : GenericRepo<Timing>, ITimeRepo
    {
        private readonly AppDbContext _context;
        public TimeRepo(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Timing> GetTicketByTimeId(int timeId)
        {
            var Time = await _context.Timings.Include(t=>t.Tickets).Where(t=>t.Id == timeId).SingleOrDefaultAsync();

            return Time;
        } 
    }
}
