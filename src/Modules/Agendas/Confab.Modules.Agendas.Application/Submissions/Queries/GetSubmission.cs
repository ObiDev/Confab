using Confab.Modules.Agendas.Application.Submissions.DTO;
using Confab.Shared.Abstractions.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Confab.Modules.Agendas.Application.Submissions.Queries
{
    public class GetSubmission : IQuery<SubmissionDto>
    {
        public Guid Id { get; set; }
    }
}
