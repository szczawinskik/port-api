using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Validation.Interfaces;
using Web.ViewModels;

namespace Web.Validation.Validators
{
    public class ShipViewModelValidator : IValidator<ShipViewModel>
    {
        private readonly IValidator<ScheduleViewModel> scheduleValidator;
        public ShipViewModelValidator(IValidator<ScheduleViewModel> scheduleValidator)
        {
            this.scheduleValidator = scheduleValidator;
        }
        public List<string> ErrorList { get; private set; }

        public bool IsValid(ShipViewModel item)
        {
            var valid = true;
            ErrorList = new List<string>();
            foreach (var schedule in item.Schedules)
            {
                valid = scheduleValidator.IsValid(schedule) || valid;
            }
            scheduleValidator.IsValid(item.ClosestSchedule);
            ErrorList.AddRange(scheduleValidator.ErrorList);

            return !ErrorList.Any();
        }
    }
}
