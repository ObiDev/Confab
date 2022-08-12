using Confab.Shared.Abtractions.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Confab.Modules.Conferences.Core.Exceptions
{
    internal class HostNotFoundException : ConfabException
    {
        public Guid Id { get; set; }

        public HostNotFoundException(Guid id) : base($"Host with id '{id}' was not found.")
        {
            Id = id;
        }
    }
}
