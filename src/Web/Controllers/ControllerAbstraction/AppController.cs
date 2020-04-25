using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;

namespace Web.Controllers.ControllerAbstraction
{
    public abstract class AppController<TResultType, TAddType> : AddController<TResultType, TAddType>
    {    
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public abstract IActionResult Update(TResultType entity);
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public abstract IActionResult Delete(int id);
    }
}
