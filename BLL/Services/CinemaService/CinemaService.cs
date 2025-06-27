using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTO.Cinema;
using BLL.Factory.CinemaFactory;
using BLL.Services.ActorService;
using BLL.Services.FileService;
using DAL.Data.Models;
using DAL.Data.Repositories.Calsses;
using DAL.Data.Repositories.Intrfaces;

namespace BLL.Services.CinemaService
{
    public class CinemaService(ICinemaRepo _cinemaRepo, IFileService _fileservice) : ICenimaService
    {
        public async Task AddCinema(CinemaAddDto cinemaDto)
        {
          
            try
            {
                var Logo = await _fileservice.UploadFileAsync(cinemaDto.Logo,"Images/Cinema");

                Cinema cinema = new Cinema
                {
                    Id = cinemaDto.Id,
                    Name = cinemaDto.Name,
                    Address = cinemaDto.Address,
                    Capacity = cinemaDto.Capacity,
                    Logo = Logo
                };
                await _cinemaRepo.AddAsync(cinema);

            }catch(Exception ex)
            {
                throw new Exception("Insert Vaild Data");
            }
           
        }

        public async Task RemoveCinema(int Id)
        {
            try
            {
                var cinema = await _cinemaRepo.FindByIdAsync(Id);
                if (cinema == null)
                    throw new Exception("No actor Found");
                if (!string.IsNullOrEmpty(cinema.Logo))
                {
                    var fileRemoved = _fileservice.DeleteFile(cinema.Logo);

                }
                await _cinemaRepo.Remove(cinema);


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ICollection<CinemaAdminDto>> GetAllCinemas()
        {
            var cinamas = await _cinemaRepo.GetAllAsync();
            return cinamas.Select(c => c.MapToAdminDto()).ToList();
        }

        public async Task<CinemaAdminDto> GetCinemaById(int id)
        {
            var cinema = await _cinemaRepo.FindByIdAsync(id);
            return cinema.MapToAdminDto();
        }

        public async Task<ICollection<CinemaDTO>> GetCinemasWithTimes()
        {
            var cinema = await _cinemaRepo.GetAllWiithTime();
            return cinema.Select(c => c.MapToDto()).ToList();
        }

        public async Task Update(CinemaEditDto cinemaEdit)
        {
            try
            {
                var Cinema = new Cinema()
                {
                    Id = cinemaEdit.Id,
                    Name = cinemaEdit.Name,
                    Address= cinemaEdit.Address,
                    Capacity =cinemaEdit.Capacity,
                    Logo = cinemaEdit.Logo,
                };
                if (cinemaEdit.LogoFile != null)
                {

                    var delete = _fileservice.DeleteFile(cinemaEdit.Logo);
                    if (!delete)
                        throw new Exception("Error in Deleted Try Again");
                    var addPic = await _fileservice.UploadFileAsync(cinemaEdit.LogoFile,
                        "Images/Cinema");
                    if (addPic != null)
                        Cinema.Logo = addPic;

                }

                await _cinemaRepo.Update(Cinema);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        }
    }
