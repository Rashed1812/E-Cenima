using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Data.Models.Movie_Module
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public Category Category { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int Duration { get; set; }
        public string Description { get; set; }
        public double Rating { get; set; }
        public string ImageUrl { get; set; }
    }
}
