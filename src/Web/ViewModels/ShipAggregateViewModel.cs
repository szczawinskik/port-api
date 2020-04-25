using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Web.ViewModels
{
    public class ShipAggregateViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string ShipOwnerName { get; set; }
        public ScheduleViewModel ClosestSchedule { get; set; }
    }
}
