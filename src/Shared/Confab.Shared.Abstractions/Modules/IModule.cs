using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Confab.Shared.Abstractions.Modules
{
    public interface IModule
    {
        string Name { get; }
        string Path { get; }
        void Register(IServiceCollection services);
        void Use(IApplicationBuilder app);
        IEnumerable<string> Policies => null;
    }
}
