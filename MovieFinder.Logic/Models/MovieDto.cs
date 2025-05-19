using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieFinder.Logic.Models
{
    public class MovieDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Genre Genre { get; set; } = Genre.Unknown;
        public TimeSpan Duration { get; set; }
        public List<ActorDto> ActorList { get; set; } = new();
    }
}
