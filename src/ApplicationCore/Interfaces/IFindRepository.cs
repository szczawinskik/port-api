using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Interfaces
{
    public interface IFindRepository<T> where T : EntityBase
    {
        T Find(int id);
    }
}
