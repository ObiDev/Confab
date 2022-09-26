using Confab.Shared.Abstractions.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Confab.Modules.Agendas.Application.Submission.Events.External
{
    public record SpeakerCreated(Guid id, string FullName) : IEvent;
}

