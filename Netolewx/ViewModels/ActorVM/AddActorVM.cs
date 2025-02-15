using System;
using System.ComponentModel.DataAnnotations;

namespace Netolewx.ViewModels.ActorVM
{
    public class AddActorVM
    {
        
        [Required(ErrorMessage = "First name is required.")]
        [StringLength(50, ErrorMessage = "First name cannot exceed 50 characters.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        [StringLength(50, ErrorMessage = "Last name cannot exceed 50 characters.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Date of birth is required.")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [StringLength(1000, ErrorMessage = "Biography cannot exceed 1000 characters.")]
        public string Biography { get; set; }

        [Url(ErrorMessage = "Invalid URL format.")]
        public string ProfilePictureUrl { get; set; }

    }
}
