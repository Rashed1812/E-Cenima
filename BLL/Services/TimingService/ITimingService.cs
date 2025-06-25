using BLL.DTO.TIme;
using DAL.Data.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.TimingService
{
    public interface ITimeService
    {
        public Task<Timing> GetTicketByTimeId(int id);
    }
}
