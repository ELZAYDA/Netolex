﻿using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Repositiries.Interfaces
{
    public interface IMovieGenreRepo 
    {
        public void Add(MovieGenre entity);
        
    }
}
