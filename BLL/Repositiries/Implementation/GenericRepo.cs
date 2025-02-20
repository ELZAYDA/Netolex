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

        public void Delete(T entity)
        {
            _dbcontext.Set<T>().Remove(entity);
        }

        public async Task<T> GetAsync(int id)
        {
            return await _dbcontext.Set<T>().FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAllAsync(bool trackChanges = false)
        {
            return await (trackChanges
                ? _dbcontext.Set<T>().ToListAsync()
                : _dbcontext.Set<T>().AsNoTracking().ToListAsync());
        }

        public void Update(T entity)
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

        }

    }       
}
