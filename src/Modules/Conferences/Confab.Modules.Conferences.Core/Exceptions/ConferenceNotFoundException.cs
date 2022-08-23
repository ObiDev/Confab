using Confab.Shared.Abstractions.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Confab.Modules.Conferences.Core.Exceptions
{
    internal class ConferenceNotFoundException : ConfabException
    {
        public Guid Id { get; set; }

        public ConferenceNotFoundException(Guid id) : base($"Conference with id '{id}' was not found.")
        {
            Id = id;
        }
    }
}
