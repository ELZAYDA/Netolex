using System;
using System.ComponentModel.DataAnnotations;

namespace Netolewx.ViewModels.ReviewVM
{
    public class AddReviewVM
    {

        [Required]
        [Range(1, 10, ErrorMessage = "Rating must be between 1 and 10.")]
        public int Rating { get; set; }

        [Required(ErrorMessage = "Comment is required.")]
        [StringLength(1000, ErrorMessage = "Comment cannot exceed 1000 characters.")]
        public string Comment { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Required]
        public int MovieId { get; set; }
        //[Required]
        //public int UserId { get; set; }

    }
}
