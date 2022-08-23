using Confab.Shared.Abstractions.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Confab.Modules.Conferences.Core.Exceptions
{
    internal class CannotDeleteHostException : ConfabException
    {
        public CannotDeleteHostException(Guid id) : base($"Host with id '{id}' can not be deleted.")
        {
        }
    }
}
