using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTO.Cinema;
using BLL.Factory.CinemaFactory;
using DAL.Data.Repositories.Intrfaces;

namespace BLL.Services.CinemaService
{
    public class CinemaService(ICinemaRepo _cinemaRepo) : ICenimaService
    {
        public async Task<ICollection<CinemaDTO>> GetAllCinemas()
        {
            var cinamas = await _cinemaRepo.GetAllAsync();
            return cinamas.Select(c => c.MapToDto()).ToList();
        }

        public async Task<CinemaDetailsDTO> GetCinemaById(int id)
        {
            var cinema = await _cinemaRepo.FindByIdAsync(id);
            return cinema?.MapToDetailsDto();
        }

    }
}
