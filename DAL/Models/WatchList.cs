using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class WatchList : ModelBase
    {
      //  public int Id { get; set; }
        public DateTime AddedAt { get; set; }
        public WatchStatus Status { get; set; }

        // Foreign keys
        public int MovieId { get; set; }
        public int UserId { get; set; }

        // Navigation properties
        public Movie Movie { get; set; }
    }
}
