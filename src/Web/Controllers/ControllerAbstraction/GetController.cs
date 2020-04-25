using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;

namespace Web.Controllers.ControllerAbstraction
{
    [ApiController]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    public abstract class GetController<T> : ControllerBase
    {
        [HttpGet("[action]")]
        public abstract IQueryable<T> GetAll();
        [HttpGet("{id}")]
        public abstract IActionResult GetById(int id);
    }
}
