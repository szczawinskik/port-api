using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
    public interface ISenderService
    {
        void FetchAddress();
        IQueryable<Schedule> OldNotSentSchedules(DateTime now);
        Task SendArrival(Schedule schedule);
        Task SendDeparture(Schedule schedule);
    }
}
