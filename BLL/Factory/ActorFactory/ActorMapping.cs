using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTO.Actors;
using BLL.Services.ActorService;
using DAL.Data.Models;

namespace BLL.Factory.ActorFactory
{
    public static class ActorMapping
    {
        public static ActorDto ToActorDTO(this Actor actor)
        {
            return new ActorDto()
            {
                Id = actor.Id,
                FullName = actor.FullName,
                Bio = actor.Bio,
                ProfilePictureURL = actor.ProfilePictureURL
            };
        }
        public static ActorDetailsDTO ToActorDetailsDTO(this Actor actor)
        {
            return new ActorDetailsDTO()
            {
                Id = actor.Id,
                FullName = actor.FullName,
                Bio = actor.Bio,
                ProfilePictureURL = actor.ProfilePictureURL
            };
        }
    }
}
