﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.ViewModels
{
    public class ShipAggregateViewModel : ShipViewModel
    {
       public List<ScheduleViewModel> Schedules { get; set; }
    }
}
