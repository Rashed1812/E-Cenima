using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Data.Models;
using DAL.Data.Repositories.GenericRepositories;
using DAL.Data.Repositories.Intrfaces;

namespace DAL.Data.Repositories.Calsses
{
    public class ProducerRepo :GenericRepo<Producer> ,IProducerRepo
    {
        public ProducerRepo(AppDbContext context) : base(context)
        {
        }
    }
}
