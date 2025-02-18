using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Repositiries.Interfaces
{
    public interface IGenericRepo<T> where T : ModelBase
    {
        //add, update, delete, get
        T Get(int id);

        IEnumerable<T> GetAll(bool trackChanges=false);

        int Add(T entity);

        void Update(T entity);

        void Delete(T entity);
    }
}
