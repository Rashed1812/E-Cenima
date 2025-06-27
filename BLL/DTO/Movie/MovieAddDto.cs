using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Services.ActorService;
using DAL.Data.Models;
using DAL.Data.Models.Movie_Module;
using Microsoft.AspNetCore.Http;

namespace BLL.DTO.Movie
{
    public class MovieAddDto
    {
        public string Name { get; set; }

        public string Description { get; set; }
        public IFormFile ImageFile { get; set; }

        public DateTime ReleaseDate { get; set; }
        public string TrailerURL { get; set; }
        public Category MovieCategory { get; set; }
    }
}
