using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class ApplicationUser 
    { 
            // Add custom user properties
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string ProfilePicture { get; set; }
            public DateTime RegisteredAt { get; set; }

            // Navigation properties
            public virtual ICollection<Review> Reviews { get; set; }
            public virtual ICollection<WatchList> WatchLists { get; set; }
        
    }
}
