using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Director : ModelBase
    {
        //public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Biography { get; set; }
        public string ProfilePictureUrl { get; set; }

        // Navigation property
        public ICollection<MovieDirector> MovieDirectors { get; set; }=new HashSet<MovieDirector>();
    }
}
