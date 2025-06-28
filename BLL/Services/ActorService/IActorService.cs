using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTO.Actors;
using DAL.Data.Models;
using DAL.Data.Repositories.GenericRepositories;

namespace BLL.Services.ActorService
{
    public interface IActorService 
    {
        Task<IEnumerable<ActorDto>> GetAllAsync();
        Task<ActorDetailsDTO> GetByIdAsync(int id);
        Task<ICollection<ActorDto>> GetActorsByNameAsync(string name);
        Task AddAsync(ActorAddDto actorAddDto);
        Task RemoveAsync(int Id);
        Task EditAsync(ActorEditDto actorDto);

    }
}
