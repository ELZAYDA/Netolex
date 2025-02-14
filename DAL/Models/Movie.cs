using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int ReleaseYear { get; set; }
        public int Duration { get; set; } // in minutes
        public string PosterUrl { get; set; }
        public string TrailerUrl { get; set; }
        public double Rating { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        // Navigation properties
        public ICollection<MovieGenre> MovieGenres { get; set; }
        public ICollection<MovieActor> MovieActors { get; set; }
        public ICollection<MovieDirector> MovieDirectors { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public ICollection<WatchList> WatchLists { get; set; }
    }
}
// Enum for WatchList Status
public enum WatchStatus
{
    WantToWatch,
    Watching,
    Watched,
    Dropped
}