using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entities
{
    public class Ship: EntityBase
    {
        public string Name { get; set; }
        public virtual IEnumerable<Schedule> Schedules { get; set; }
        public ShipOwner ShipOwner { get; set; }
    }
}
