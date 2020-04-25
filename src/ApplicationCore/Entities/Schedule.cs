using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ApplicationCore.Entities
{
    public class Schedule : EntityBase
    {
        public DateTime Arrival { get; set; }
        public DateTime Departure { get; set; }
        public Ship Ship { get; set; }
    }
}
