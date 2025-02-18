using BLL.Repositiries.Interfaces;
using DAL.Contexts;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BLL.Repositiries.Implementation
{
    public class MovieGenreRepo :  IMovieGenreRepo
    {
        private readonly DbApplicationContext _dbcontext;

        public MovieGenreRepo(DbApplicationContext dbcontext) 
        {
            _dbcontext = dbcontext;
        }

        public void Add(MovieGenre entity)
        {
             _dbcontext.Add(entity);
        }
    }
}
