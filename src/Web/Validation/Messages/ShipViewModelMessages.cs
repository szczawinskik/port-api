using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Validation.Messages
{
    public class ShipViewModelMessages
    {
        public const string ClosestScheduleShouldBeInSchedules = "Termin najbliższego podejścia i odejścia musi być dodana do listy terminów";
        public const string ClosestScheduleIsNotClosest = "Termin najbliższego podejścia i odejścia nie jest zgodny z podaną listą terminów";
    }
}
