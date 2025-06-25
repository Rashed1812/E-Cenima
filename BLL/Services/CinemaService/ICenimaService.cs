using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTO.Cinema;

namespace BLL.Services.CinemaService
{
    public interface ICenimaService
    {
        Task<ICollection<CinemaDTO>> GetAllCinemas();
        Task<CinemaDetailsDTO> GetCinemaById(int id);

        Task<ICollection<CinemaDTO>> GetCinemasWithTimes();
    }
}
