using Confab.Modules.Agendas.Application;
using Confab.Modules.Agendas.Domain;
using Confab.Modules.Agendas.Infrastructure;
using Confab.Shared.Abstractions.Modules;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Confab.Modules.Agendas.Api
{
    internal class AgendasModule : IModule
    {
        public const string BasePath = "agendas-module";

        public string Name { get; } = "Agendas";
        public string Path => BasePath;
        public IEnumerable<string> Policies { get; } = new[] { "agendas" };

        public void Register(IServiceCollection services)
        {
            services
                .AddDomain()
                .AddApplication()
                .AddIntrastructure();
        }

        public void Use(IApplicationBuilder app)
        {
        }
    }
}
