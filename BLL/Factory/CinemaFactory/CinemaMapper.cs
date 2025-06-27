using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTO.Cinema;
using DAL.Data.Models;
using DAL.Data.Models.Movie_Module;

namespace BLL.Factory.CinemaFactory
{
    public static class CinemaMapper
    {
        public static CinemaDTO MapToDto(this Cinema cinema)
        {
            if (cinema == null) return null;
            return new CinemaDTO
            {
                Id = cinema.Id,
                Name = cinema.Name,
                Address = cinema.Address,
                Logo = cinema.Logo,
                Showtimes = cinema.Showtimes
            };
        }
        public static CinemaAdminDto MapToAdminDto(this Cinema cinema)
        {
            if (cinema == null) return null;
            return new CinemaAdminDto
            {
                Id = cinema.Id,
                Name = cinema.Name,
                Address = cinema.Address,
                Logo = cinema.Logo,
                Capacity = cinema.Capacity,
            };
        }
        public static CinemaDetailsDTO MapToDetailsDto(this Cinema cinema)
        {
            if (cinema == null) return null;
            return new CinemaDetailsDTO()
            {
                Id = cinema.Id,
                Name = cinema.Name,
                Address = cinema.Address,
                Logo = cinema.Logo,
                Showtimes = cinema.Showtimes
            };
        }
    }
}
