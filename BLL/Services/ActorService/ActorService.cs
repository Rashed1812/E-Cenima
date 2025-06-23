using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTO.Actors;
using BLL.Factory.ActorFactory;
using DAL.Data.Models;
using DAL.Data.Repositories.Intrfaces;

namespace BLL.Services.ActorService
{
    public class ActorService(IActorRepo _actorRepo) : IActorService
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


    }
}
