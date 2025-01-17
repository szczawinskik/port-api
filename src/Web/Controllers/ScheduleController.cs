﻿using ApplicationCore.Entities;
using AutoMapper;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Controllers.ControllerAbstraction;
using Web.Validation.Interfaces;
using Web.ViewModels;

namespace Web.Controllers
{

    [Route("api/[controller]")]
    public class ScheduleController : AppController<ScheduleViewModel, ScheduleViewModel>
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

        /// <summary>
        /// Akcja służąca do dodawania nowych harmonogramów dla statku
        /// </summary>
        /// <remarks>Jako parametry przyjmuje dane harmonogramu oraz id statku</remarks>
        [HttpPost("{shipId}")]
        public override IActionResult Add(ScheduleViewModel model, int shipId)
        {
            if (validator.IsValid(model))
            {
                var entity = mapper.Map<Schedule>(model);
                if (service.Add(entity, shipId))
                {
                    return Ok();
                }
                return BadRequest();
            }
            return BadRequest(validator.ErrorList);
        }

        [HttpPost("[action]/{shipId}")]
        public IActionResult BulkAdd(ScheduleViewModel[] models, int shipId)
        {
            var valid = true;
            foreach (var model in models)
            {
                if (!validator.IsValid(model))
                {
                    valid = false;
                }

            }
            if (!valid)
            {
                return BadRequest(validator.ErrorList);

            }
            var entities = mapper.Map<List<Schedule>>(models);
            if (service.AddRange(entities, shipId))
            {
                return Ok();
            }
            return BadRequest();
        }

        public override IActionResult Delete(int id)
        {
            if (service.Delete(id))
            {
                return Ok();
            }
            return BadRequest();
        }

        public override IQueryable<ScheduleViewModel> GetAll()
        {
            return mapper.ProjectTo<ScheduleViewModel>(service.GetAll(), null);
        }

        public override IActionResult GetById(int id)
        {
            var entity = service.Find(id);
            if (entity != null)
            {
                return Ok(mapper.Map<ScheduleViewModel>(entity));
            }
            return BadRequest();
        }

        /// <summary>
        /// Akcja służąca do modyfikowania harmonogramu 
        /// </summary>
        /// <remarks>Jako parametry przyjmuje dane harmonogramu</remarks>
        public override IActionResult Update(ScheduleViewModel model)
        {
            if (validator.IsValid(model))
            {
                var entity = mapper.Map<Schedule>(model);
                if (service.Update(entity))
                {
                    return Ok();
                }
                return BadRequest();
            }
            return BadRequest(validator.ErrorList);
        }
    }
}
