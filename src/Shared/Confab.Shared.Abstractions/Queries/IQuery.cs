using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Confab.Shared.Abstractions.Queries
{
    //marker interface
    public interface IQuery
    {
    }

    public interface IQuery<T> : IQuery
    {
    }
}
