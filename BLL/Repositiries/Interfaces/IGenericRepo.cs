using DAL.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Repositiries.Interfaces
{
    public interface IGenericRepo<T> where T : ModelBase
    {
        //add, update, delete, get
        Task<T> GetAsync(int id);

        Task <IEnumerable<T>> GetAllAsync(bool trackChanges=false);

        int Add(T entity);

        void Update(T entity);

        void Delete(T entity);
    }
}
