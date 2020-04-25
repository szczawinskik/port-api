using ApplicationCore.Entities;
using AutoMapper;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Validation.Interfaces;
using Web.ViewModels;

namespace Web.Controllers
{

    [Route("[controller]")]
    public class ScheduleController : AppController<ScheduleViewModel>
    {
        private readonly IMapper mapper;
        private readonly IValidator<ScheduleViewModel> validator;
        private readonly IService<Schedule> service;

        public ScheduleController(IValidator<ScheduleViewModel> validator, IService<Schedule> service,
            IMapper mapper)
        {
            this.mapper = mapper;
            this.validator = validator;
            this.service = service;
        }
        public override IActionResult Add(ScheduleViewModel model)
        {
            if (validator.IsValid(model))
            {
                var entity = mapper.Map<Schedule>(model);
                if(service.Add(entity))
                {
                    return Ok();
                }
                return BadRequest();
            }
            return BadRequest(validator.ErrorList);
        }

        public override void Delete(ScheduleViewModel entity)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<ScheduleViewModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public override ScheduleViewModel GetById(int id)
        {
            throw new NotImplementedException();
        }

        public override void Update(ScheduleViewModel entity)
        {
            throw new NotImplementedException();
        }
    }
}
