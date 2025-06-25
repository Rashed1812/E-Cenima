using DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Data.Repositories.Intrfaces
{
    public interface ITimeRepo
    {
        Task<Timing> GetTicketByTimeId(int timeId);

    }
}
