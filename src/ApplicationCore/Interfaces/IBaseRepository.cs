﻿using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApplicationCore.Interfaces
{
    public interface IBaseRepository<T> where T : EntityBase
    {
        IQueryable<T> GetAll();
        T Find(int id);
        void Add(T entity, int parentId);
        void Update(T entity);
        void Delete(int id);
    }
}
