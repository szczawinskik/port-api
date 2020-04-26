using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using AutoMapper;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Web.Controllers.ControllerAbstraction;
using Web.Validation.Interfaces;
using Web.ViewModels;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    public class ShipController : AddController<ShipAggregateViewModel, ShipViewModel>
    {
        private readonly IMapper mapper;
        private readonly IService<Ship> service;
        private readonly IValidator<ShipViewModel> validator;

        public ShipController(IService<Ship> service, IMapper mapper, IValidator<ShipViewModel> validator)
        {
            this.mapper = mapper;
            this.service = service;
            this.validator = validator;
        }

        [HttpPost("{shipOwnerId}")]
        public override IActionResult Add(ShipViewModel model, int shipOwnerId)
        {
            if (validator.IsValid(model))
            {
                var entity = mapper.Map<Ship>(model);
                if (service.Add(entity, shipOwnerId))
                {
                    return Ok();
                }
                return BadRequest();
            }
            return BadRequest(validator.ErrorList);
        }

        public override IQueryable<ShipAggregateViewModel> GetAll()
        {
            return mapper.ProjectTo<ShipAggregateViewModel>(service.GetAll(), null);
        }

        public override IActionResult GetById(int id)
        {
            var entity = service.Find(id);
            if (entity != null)
            {
                return Ok(mapper.Map<ShipViewModel>(entity));
            }
            return BadRequest();
        }
    }
}
