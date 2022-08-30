using Confab.Shared.Abstractions.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Confab.Modules.Conferences.Core.Events
{
    public record ConferenceCreated(Guid Id, string Name, int? ParticipantLimit, DateTime From, DateTime To) : IEvent;
}
