using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Controllers.ControllerAbstraction
{
    public abstract class AddController<TResultType, TAddType>: GetController<TResultType>
    {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public abstract IActionResult Add(TAddType entity, int parentId);
    }
}
