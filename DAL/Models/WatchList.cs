using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
   public class WatchList : ModelBase
    {
        //public int Id { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public int MovieId { get; set; }
        public Movie Movie { get; set; }

        public DateTime DateAdded { get; set; } = DateTime.UtcNow;
    }
}
