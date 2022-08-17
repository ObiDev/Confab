using Confab.Shared.Abtractions.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Confab.Shared.Infrastructure.Exceptions
{
    internal interface IExcepionCompositionRoot
    {
        ExceptionResponse Map(Exception exception);
    }
}
