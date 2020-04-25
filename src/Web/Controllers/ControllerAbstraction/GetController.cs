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
    public abstract class GetController<TResultType> : ControllerBase
    {
        [HttpGet("[action]")]
        public abstract IQueryable<TResultType> GetAll();
        [HttpGet("{id}")]
        public abstract IActionResult GetById(int id);
    }
}
