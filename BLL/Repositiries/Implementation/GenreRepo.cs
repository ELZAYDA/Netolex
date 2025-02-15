using BLL.Repositiries.Interfaces;
using DAL.Contexts;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Repositiries.Implementation
{
    public class GenreRepo : GenericRepo<Genre>, IGenreRepo
    {
        public GenreRepo(DbApplicationContext dbcontext) : base(dbcontext)
        {
        }
    }
}
