using Confab.Shared.Abtractions.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Confab.Modules.Speakers.Core.Exceptions
{
    internal class SpeakerAlreadyExistsException : ConfabException
    {
        public SpeakerAlreadyExistsException(Guid id) : base($"Speker with email '{id}' already exists.")
        {
        }
    }
}
