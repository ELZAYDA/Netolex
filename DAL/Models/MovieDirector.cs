using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class MovieDirector
    {
        public int MovieId { get; set; }
        public int DirectorId { get; set; }

        // Navigation properties
        public Movie Movie { get; set; }
        public Director Director { get; set; }
    }
}
