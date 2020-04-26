using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApplicationCore.Interfaces
{
    public interface IScheduleRepository : IBaseRepository<Schedule>
    {
        IQueryable<Schedule> GetAllWithShips();
    }
}
