using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Review : ModelBase
    {
       // public int Id { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedAt { get; set; }

        // Foreign keys
        public int MovieId { get; set; }
        public int UserId { get; set; }

        // Navigation properties
        public Movie Movie { get; set; }
        public User User { get; set; }
    }
}
