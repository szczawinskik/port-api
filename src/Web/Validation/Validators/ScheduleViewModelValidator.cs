using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Validation.Interfaces;
using Web.Validation.Messages;
using Web.ViewModels;

namespace Web.Validation.Validators
{
    public class ScheduleViewModelValidator : IValidator<ScheduleViewModel>
    {
        public ICollection<string> ErrorList { get; private set; }

        public bool IsValid(ScheduleViewModel item)
        {
            ErrorList = new List<string>();
            if (item.Arrival > item.Departure)
            {
                ErrorList.Add(ScheduleViewModelMessages.ArrivalLaterThanDeparture);
            }
            return !ErrorList.Any();
        }
    }
}
