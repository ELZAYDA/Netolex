using BLL.Repositiries.Interfaces;
using DAL.Contexts;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Repositiries.Implementation
{
    public class GenericRepo<T> : IGenericRepo<T> where T : ModelBase
    {
        private readonly DbApplicationContext _dbcontext;

        public GenericRepo(DbApplicationContext dbcontext) {
            _dbcontext=dbcontext;
        }

        public int Add(T entity)
        {
            _dbcontext.Set<T>().Add(entity);
            return _dbcontext.SaveChanges();
        }

        public bool Delete(T entity)
        {
            _dbcontext.Set<T>().Remove(entity);
            return _dbcontext.SaveChanges()>0;
        }

        public T Get(int id)
        {
            return _dbcontext.Find<T>(id);
        }

        public IEnumerable<T> GetAll()
        {
            return _dbcontext.Set<T>().AsNoTracking().ToList();
        }

        public bool Update(T entity)
        {
            _dbcontext.Set<T>().Update(entity);
            return _dbcontext.SaveChanges()>0;
        }
    }
}
