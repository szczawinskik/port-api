using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.ViewModels
{
    public class ScheduleViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Arrival { get; set; }
        public DateTime Departure { get; set; }
    }
}
