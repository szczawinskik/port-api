using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;

namespace Web.Controllers
{

    public abstract class AppController<T> : GetController<T>
    {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public abstract IActionResult Add(T entity);
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public abstract IActionResult Update(T entity);
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public abstract IActionResult Delete(int id);
    }
}
