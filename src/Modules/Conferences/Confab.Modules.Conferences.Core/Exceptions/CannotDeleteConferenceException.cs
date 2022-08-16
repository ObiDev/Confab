using Confab.Shared.Abtractions.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Confab.Modules.Conferences.Core.Exceptions
{
    internal class CannotDeleteConferenceException : ConfabException
    {
        public CannotDeleteConferenceException(Guid id) : base($"Conference with id '{id}' can not be deleted.")
        {
        }
    }
}
