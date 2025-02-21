using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required(ErrorMessage = "First name is required")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "First name must be between 2 and 50 characters")]
        [RegularExpression(@"^[A-Za-z]+$", ErrorMessage = "First name can only contain letters")]
        public string FName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Last name must be between 2 and 50 characters")]
        [RegularExpression(@"^[A-Za-z]+$", ErrorMessage = "Last name can only contain letters")]
        public string LName { get; set; }

        [Required(ErrorMessage = "You must agree to the terms and conditions")]
        [RegularExpression(@"true", ErrorMessage = "You must agree to the terms and conditions")]
        public string AgreeToTerms { get; set; }

        //public virtual ICollection<WatchList> WatchlistItems { get; set; }=new HashSet<WatchList>();

    }
}
