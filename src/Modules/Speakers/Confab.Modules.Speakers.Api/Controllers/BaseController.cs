using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Confab.Modules.Speakers.Api.Controllers
{
    [ApiController]
    [Route(SpeakersModule.BasePath + "/[controller]")]
    internal class BaseController : ControllerBase
    {
        protected ActionResult<T> OkOrNotFound<T>(T model)
        {
            if (model is null)
                return NotFound();

            return Ok(model);
        }
    }
}
