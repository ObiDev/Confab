using Confab.Modules.Speakers.Core;
using Confab.Shared.Abtractions.Modules;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Confab.Modules.Speakers.Api
{
    internal class SpeakersModule : IModule
    {
        public const string BasePath = "speakers-module";
        public string Name { get; } = "Speakers";
        public string Path { get; }

        public void Register(IServiceCollection services)
        {
            services.AddCore();
        }

        public void Use(IApplicationBuilder app)
        {
        }
    }
}
