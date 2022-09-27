using Confab.Modules.Agendas.Application.Submissions.Exceptions;
using Confab.Modules.Agendas.Domain.Submissions.Repositories;
using Confab.Shared.Abstractions.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Confab.Modules.Agendas.Application.Submissions.Commands.Handlers
{
    public sealed class RejectSubmissionHandler : ICommandHandler<RejectSubmission>
    {
        private readonly ISubmissionRepository _submissionRepository;

        public RejectSubmissionHandler(ISubmissionRepository submissionRepository)
        {
            _submissionRepository = submissionRepository;
        }

        public async Task HandleAsync(RejectSubmission command)
        {
            var submission = await _submissionRepository.GetAsync(command.id);

            if (submission is null)
                throw new SubmissionNotFoundException(command.id);

            submission.Reject();

            await _submissionRepository.UpdateAsync(submission);

        }
    }
}
