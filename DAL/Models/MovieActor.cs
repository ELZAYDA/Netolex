using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class MovieActor
    {
        public int MovieId { get; set; }
        public int ActorId { get; set; }
        public string Role { get; set; }  // Character name

        // Navigation properties
        public Movie Movie { get; set; }
        public Actor Actor { get; set; }
    }
}
