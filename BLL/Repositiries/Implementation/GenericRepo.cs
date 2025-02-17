using BLL.Repositiries.Interfaces;
using DAL.Contexts;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace BLL.Repositiries.Implementation
{
    public class GenericRepo<T> : IGenericRepo<T> where T : ModelBase
    {
        protected readonly DbApplicationContext _dbcontext;

        public GenericRepo(DbApplicationContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public int Add(T entity)
        {
            _dbcontext.Set<T>().Add(entity);
            return _dbcontext.SaveChanges();
        }

        public bool Delete(T entity)
        {
            _dbcontext.Set<T>().Remove(entity);
            return _dbcontext.SaveChanges() > 0;
        }

        public T Get(int id)
        {
            return _dbcontext.Set<T>().Find(id);
        }

        public IEnumerable<T> GetAll(bool trackChanges = false)
        {
            return trackChanges
                ? _dbcontext.Set<T>().ToList()
                : _dbcontext.Set<T>().AsNoTracking().ToList();
        }

        public bool Update(T entity)
        {
            var existingEntity = _dbcontext.Set<T>().Find(entity.Id);
            if (existingEntity != null)
            {
                _dbcontext.Entry(existingEntity).CurrentValues.SetValues(entity);
            }
            else
            {
                _dbcontext.Set<T>().Update(entity);
            }

            return _dbcontext.SaveChanges() > 0;
        }

    }       
}
