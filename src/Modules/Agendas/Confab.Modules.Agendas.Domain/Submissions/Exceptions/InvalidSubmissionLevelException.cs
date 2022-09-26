using Confab.Shared.Abstractions.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Confab.Modules.Agendas.Domain.Submissions.Exceptions
{
    public class InvalidSubmissionLevelException : ConfabException
    {
        public Guid SubmissionId { get; }

        public InvalidSubmissionLevelException(Guid submissionId) : base($"Submission with ID '{submissionId}' defines invalid level. ")
        {
            SubmissionId = submissionId;
        }
    }
}
