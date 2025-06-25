using BLL.DTO.TIme;
using BLL.Services.Movies;
using DAL.Data.Models;
using DAL.Data.Repositories.Intrfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.TimingService
{
        public class TimeService(ITimeRepo _TimeRepo) : ITimeService
    {
        public async Task<Timing> GetTicketByTimeId(int id)
        {
            var time = await _TimeRepo.GetTicketByTimeId(id);
            return time;
        }

     
    }
    
}
