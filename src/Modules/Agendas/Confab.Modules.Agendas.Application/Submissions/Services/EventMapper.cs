using Confab.Modules.Agendas.Application.Submissions.Events;
using Confab.Modules.Agendas.Domain.Submissions.Const;
using Confab.Modules.Agendas.Domain.Submissions.Events;
using Confab.Shared.Abstractions.Kernel;
using Confab.Shared.Abstractions.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Confab.Modules.Agendas.Application.Services
{
    public class EventMapper : IEventMapper
    {
        public IMessage Map(IDomainEvent @event)
            => @event switch
            {
                SubmissionAdded e => new SubmissionCreated(e.Submisstion.Id),
                SubmissionStatusChanged 
                    { Status: SubmissionStatus.Approved } e => new SubmissionApproved(e.Submission.Id),
                SubmissionStatusChanged 
                    { Status: SubmissionStatus.Rejected } e => new SubmissionRejected(e.Submission.Id),
                _ => null
            };

        public IEnumerable<IMessage> MapAll(IEnumerable<IDomainEvent> events)
            => events.Select(Map);
    }
}
