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
    public sealed class ApproveSubmissionHandler : ICommandHandler<ApproveSubmission>
    {
        private readonly ISubmissionRepository _submissionRepository;

        public ApproveSubmissionHandler(ISubmissionRepository submissionRepository)
        {
            _submissionRepository = submissionRepository;
        }

        public async Task HandleAsync(ApproveSubmission command)
        {
            var submission = await _submissionRepository.GetAsync(command.id);

            if (submission is null)
                throw new SubmissionNotFoundException(command.id);

            submission.Approve();

            await _submissionRepository.UpdateAsync(submission);

        }
    }
}
