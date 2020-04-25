using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ApplicationCore.Entities;

namespace Infrastructure.Interfaces
{
    public interface IService<T> where T : EntityBase
    {
        bool Add(T item, int parentId);
        IQueryable<T> GetAll();
        bool Delete(int id);
        T Find(int idToFind);
        bool Update(T entity);
    }
}
