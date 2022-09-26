using Confab.Shared.Abstractions.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Confab.Modules.Agendas.Domain.Submissions.Exceptions
{
    public class EmptySubmissionTitleException : ConfabException
    {
        public Guid SubmissionId { get; }

        public EmptySubmissionTitleException(Guid submissionId) : base($"Submission with ID '{submissionId}' defines empty title. ")
        {
            SubmissionId = submissionId;
        }
    }
}
