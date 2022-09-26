using Confab.Modules.Agendas.Api.Controllers;
using Microsoft.AspNetCore.Authorization;

namespace Confab.Modules.Conferences.Api.Controllers
{
    [Authorize(Policy = Policy)]
    internal class AgendasController : BaseController
    {
        private const string Policy = "agendas";
        
    }
}
