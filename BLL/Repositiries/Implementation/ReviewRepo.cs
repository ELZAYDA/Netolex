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
    public class ReviewRepo : GenericRepo<Review>, IReviewRepo
    {
        public ReviewRepo(DbApplicationContext dbcontext) : base(dbcontext)
        {
        }
    }
}
