﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Netolewx.ViewModels.GenreVM
{
    public class DetailsEditGenreVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
        public string Name { get; set; }

        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string Description { get; set; }

    }
}
