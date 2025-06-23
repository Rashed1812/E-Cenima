using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTO.Actors;

namespace BLL.Services.ActorService
{
    public interface IActorService
    {
        Task<ICollection<ActorDto>> GetAllAsync();
        Task<ActorDetailsDTO> GetByIdAsync(int id);
        Task<ICollection<ActorDto>> GetActorsByNameAsync(string name);
    }
}
