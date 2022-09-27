using Confab.Shared.Abstractions.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Confab.Modules.Agendas.Application.Submissions.Exceptions
{
    internal class SubmissionNotFoundException : ConfabException
    {
        public Guid SubmissionId { get; }

        public SubmissionNotFoundException(Guid submissionId)
            : base($"Submission with ID '{submissionId}' was not found. ")
        {
            SubmissionId = submissionId;
        }
    }
}
