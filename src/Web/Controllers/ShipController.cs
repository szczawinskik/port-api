using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using AutoMapper;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Web.Controllers.ControllerAbstraction;
using Web.ViewModels;

namespace Web.Controllers
{
    public class ShipController : GetController<ShipViewModel>
    {
        private readonly IMapper mapper;
        private readonly IService<Ship> service;

        public override IQueryable<ShipViewModel> GetAll()
        {
            return mapper.ProjectTo<ShipViewModel>(service.GetAll(), null);
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
