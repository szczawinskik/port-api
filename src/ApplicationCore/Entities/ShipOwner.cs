using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entities
{
    public class ShipOwner: EntityBase
    {
        public string Name { get; set; }
        public virtual IEnumerable<Ship> Ships { get; set; }
    }
}
