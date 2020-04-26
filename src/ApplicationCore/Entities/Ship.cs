using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ApplicationCore.Entities
{
    public class Ship: EntityBase
    {
        public string Name { get; set; }
        [ForeignKey("ShipId")]
        public virtual IEnumerable<Schedule> Schedules { get; set; }
        public virtual ShipOwner ShipOwner { get; set; }
        public virtual Schedule ClosestSchedule { get; set; }
    }
}
