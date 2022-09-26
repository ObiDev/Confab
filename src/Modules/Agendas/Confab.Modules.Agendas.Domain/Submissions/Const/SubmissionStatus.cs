using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Confab.Modules.Agendas.Domain.Submissions.Const
{
    public static class SubmissionStatus
    {
        public const string Pending = nameof(Pending);
        public const string Approved = nameof(Approved);
        public const string Rejected = nameof(Rejected);

        public static bool IsValid(string status)
            => status is Pending  || status is Approved || status is Rejected;
    }
}
