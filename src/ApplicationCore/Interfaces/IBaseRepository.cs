using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApplicationCore.Interfaces
{
    public interface IBaseRepository<T>: IFindRepository<T>  where T : EntityBase
    {
        IQueryable<T> GetAll();
        void Add(T entity);
        void Update(T entity);
        void Delete(int id);
    }
}
