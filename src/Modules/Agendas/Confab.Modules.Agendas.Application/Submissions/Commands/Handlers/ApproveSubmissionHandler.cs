using Confab.Modules.Agendas.Application.Services;
using Confab.Modules.Agendas.Application.Submissions.Exceptions;
using Confab.Modules.Agendas.Domain.Submissions.Repositories;
using Confab.Shared.Abstractions.Commands;
using Confab.Shared.Abstractions.Kernel;
using Confab.Shared.Abstractions.Messaging;
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
        private readonly IDomaintEventDispatcher _domaintEventDispatcher;
        private readonly IEventMapper _eventMapper;
        private readonly IMessageBroker _messageBroker;

        public ApproveSubmissionHandler(ISubmissionRepository submissionRepository,
            IDomaintEventDispatcher domaintEventDispatcher,
            IMessageBroker messageBroker,
            IEventMapper eventMapper)
        {
            _submissionRepository = submissionRepository;
            _domaintEventDispatcher = domaintEventDispatcher;
            _messageBroker = messageBroker;
            _eventMapper = eventMapper;
        }

        public async Task HandleAsync(ApproveSubmission command)
        {
            var submission = await _submissionRepository.GetAsync(command.id);

            if (submission is null)
                throw new SubmissionNotFoundException(command.id);

            submission.Approve();

            await _submissionRepository.UpdateAsync(submission);
            await _domaintEventDispatcher.DispatchAsync(submission.Events.ToArray());

            var events = _eventMapper.MapAll(submission.Events);
            await _messageBroker.PublishAsync(events.ToArray());

        }
    }
}
