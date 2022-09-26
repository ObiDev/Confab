using Confab.Shared.Abstractions.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Confab.Modules.Speakers.Core.Events
{
    public record SpeakerCreated(Guid id, string FullName) : IEvent;
}
