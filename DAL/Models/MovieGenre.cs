﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class MovieGenre 
    {
        public int MovieId { get; set; }
        public int GenreId { get; set; }

        // Navigation properties
        public Movie Movie { get; set; }
        public Genre Genre { get; set; }
    }
}
