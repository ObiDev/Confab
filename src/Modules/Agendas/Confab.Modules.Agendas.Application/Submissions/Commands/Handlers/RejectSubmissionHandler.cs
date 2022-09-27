using Confab.Modules.Agendas.Application.Submissions.Exceptions;
using Confab.Modules.Agendas.Domain.Submissions.Repositories;
using Confab.Shared.Abstractions.Commands;
using Confab.Shared.Abstractions.Kernel;
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
        private readonly IDomaintEventDispatcher _domaintEventDispatcher;

        public RejectSubmissionHandler(ISubmissionRepository submissionRepository, IDomaintEventDispatcher domaintEventDispatcher)
        {
            _submissionRepository = submissionRepository;
            _domaintEventDispatcher = domaintEventDispatcher;
        }

        public async Task HandleAsync(RejectSubmission command)
        {
            var submission = await _submissionRepository.GetAsync(command.id);

            if (submission is null)
                throw new SubmissionNotFoundException(command.id);

            submission.Reject();

            await _submissionRepository.UpdateAsync(submission);
            await _domaintEventDispatcher.DispatchAsync(submission.Events.ToArray());


        }
    }
}
