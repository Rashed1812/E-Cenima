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
        Task<ICollection<CinemaAdminDto>> GetAllCinemas();
        Task<CinemaAdminDto> GetCinemaById(int id);

        Task<ICollection<CinemaDTO>> GetCinemasWithTimes();
        Task AddCinema(CinemaAddDto cinemaDTO);
        Task RemoveCinema(int Id);
        Task Update(CinemaEditDto cinemaEdit);
    }
}
