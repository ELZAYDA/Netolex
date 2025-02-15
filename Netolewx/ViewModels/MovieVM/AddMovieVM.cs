using System;
using System.ComponentModel.DataAnnotations;

namespace Netolewx.ViewModels.MovieVM.MovieVM
{
    public class AddMovieVM
    {
        [Required(ErrorMessage = "Title is required.")]
        [StringLength(150, ErrorMessage = "Title cannot exceed 150 characters.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Release Year is required.")]
        [Range(1888, 2100, ErrorMessage = "Release Year must be between 1888 and 2100.")]
        public int ReleaseYear { get; set; }

        [Required(ErrorMessage = "Duration is required.")]
        [Range(1, 500, ErrorMessage = "Duration must be between 1 and 500 minutes.")]
        public int Duration { get; set; }

        [Required(ErrorMessage = "Poster URL is required.")]
        [Url(ErrorMessage = "Please enter a valid URL for the poster.")]
        public string PosterUrl { get; set; }

        [Required(ErrorMessage = "Trailer URL is required.")]
        [Url(ErrorMessage = "Please enter a valid URL for the trailer.")]
        public string TrailerUrl { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
