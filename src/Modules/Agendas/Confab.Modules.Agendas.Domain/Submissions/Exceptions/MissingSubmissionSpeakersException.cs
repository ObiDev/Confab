﻿using Confab.Shared.Abstractions.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Confab.Modules.Agendas.Domain.Submissions.Exceptions
{
    internal class MissingSubmissionSpeakersException : ConfabException
    {
        public Guid SubmissionId { get; }

        public MissingSubmissionSpeakersException(Guid submissionId) 
            : base($"Submission with ID '{submissionId}' has missing speakers. ")
        {
            SubmissionId = submissionId;
        }
    }
}
