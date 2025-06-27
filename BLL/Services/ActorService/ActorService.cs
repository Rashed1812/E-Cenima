using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTO.Actors;
using BLL.Factory.ActorFactory;
using BLL.Services.FileService;
using DAL.Data.Models;
using DAL.Data.Repositories.GenericRepositories;
using DAL.Data.Repositories.Intrfaces;

namespace BLL.Services.ActorService
{
    public class ActorService(IActorRepo _actorRepo ,IFileService _fileService) : IActorService
    {

        public async Task<ICollection<ActorDto>> GetAllAsync()
        {
            var actors = await _actorRepo.GetAllAsync();
            return actors.Select(a => a.ToActorDTO()).ToList();
        }
        public async Task<ActorDetailsDTO> GetByIdAsync(int id)
        {
            var actor = await _actorRepo.FindByIdAsync(id);
            return actor?.ToActorDetailsDTO();
        }
        public async Task<ICollection<ActorDto>> GetActorsByNameAsync(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Name cannot be null or empty", nameof(name));
            }
            var actors = await _actorRepo.FindAsync(a => a.FullName.Contains(name));
            return actors.Select(a => new ActorDto
            {
                Id = a.Id,
                FullName = a.FullName,
                Bio = a.Bio,
                ProfilePictureURL = a.ProfilePictureURL
            }).ToList();
        }

        public async Task AddAsync(ActorAddDto actorDto)
        {
            if (string.IsNullOrEmpty(actorDto.FullName))
            {
                throw new ArgumentException("Name cannot be null or empty",
                    nameof(actorDto.FullName));
            }
            // Handle file upload
            if (actorDto.ProfilePictureFile != null && actorDto.ProfilePictureFile.Length > 0)
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", "Actors");

                // Create directory if it doesn't exist
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                // Generate unique filename
                var uniqueFileName = Guid.NewGuid().ToString() + "_" + actorDto.ProfilePictureFile.FileName;
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                // Save the file
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await actorDto.ProfilePictureFile.CopyToAsync(fileStream);
                }

                var actor = new Actor
                {
                    FullName = actorDto.FullName,
                    Bio = actorDto.Bio,
                    ProfilePictureURL = "\\Images\\Actors\\" + uniqueFileName
                };
                try
                {
                    await _actorRepo.AddAsync(actor);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }


        }
    
    
        public async Task RemoveAsync(int Id)
        {

            try
            {
                var actor = await _actorRepo.FindByIdAsync(Id);
                if (actor == null)
                    throw new Exception("No actor Found");
                if (!string.IsNullOrEmpty(actor.ProfilePictureURL))
                {
                   var fileRemoved=  _fileService.DeleteFile(actor.ProfilePictureURL);
              
                }
                await _actorRepo.Remove(actor);


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task EditAsync(ActorEditDto actorDto)
        {
            try
            {
                var actor = new Actor()
                {
                    Id = actorDto.Id,
                    Bio = actorDto.Bio,
                    FullName = actorDto.FullName,
                    ProfilePictureURL = actorDto.ProfilePictureURL,
                };
                if (actorDto.ProfilePictureFile!=null)
                {
                   
                        var delete = _fileService.DeleteFile(actorDto.ProfilePictureURL);
                        if (!delete)
                            throw new Exception("Error in Deleted Try Again");
                        var addPic = await _fileService.UploadFileAsync(actorDto.ProfilePictureFile,
                            "Images/Actors");
                        if(addPic!=null)
                            actor.ProfilePictureURL = addPic;
                    
                }

                await _actorRepo.Update(actor);

            }
            catch (Exception ex)
            {
                throw  new Exception(ex.Message);
            }
        }
    }



}
