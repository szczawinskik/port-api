using System;

namespace ApplicationCore.Entities
{
    public class Schedule : EntityBase
    {
        public DateTime Arrival { get; set; }
        public DateTime Departure { get; set; }
        public Ship Ship { get; set; }
        public int ShipId { get; set; }
        public bool ArrivalSent { get; set; }
        public bool DepartureSent { get; set; }
    }
}
