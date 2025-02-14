using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class User : ModelBase
    {
        //public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string ProfilePictureUrl { get; set; }
        public DateTime RegisteredAt { get; set; }
        public bool IsAdmin { get; set; }

        // Navigation properties
        public ICollection<Review> Reviews { get; set; }
        public ICollection<WatchList> WatchLists { get; set; }
    }
}
