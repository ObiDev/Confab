using Confab.Shared.Abstractions.Modules;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Confab.Shared.Infrastructure.Modules
{
    internal static class Extensions
    {
        internal static IServiceCollection AddModuleInfo(this IServiceCollection services, IList<IModule> modules)
        {
            var moduleInfoProvider = new ModuleInfoProvider();
            var moduleInfo = modules?.Select(m => new ModuleInfo(m.Name, m.Path, m.Policies ?? Enumerable.Empty<string>())) 
                ?? Enumerable.Empty<ModuleInfo>();

            moduleInfoProvider.Modules.AddRange(moduleInfo);
            services.AddSingleton(moduleInfoProvider);

            return services;
        }

        internal static void MapModuleInfo(this IEndpointRouteBuilder endpointRouteBuilder)
        {
            endpointRouteBuilder.MapGet("modules", context =>
            {
                var moduleInfoProvider = context.RequestServices.GetRequiredService<ModuleInfoProvider>();
                return context.Response.WriteAsJsonAsync(moduleInfoProvider.Modules);
            });
        }

        internal static IHostBuilder ConfigureModule(this IHostBuilder builder)
            => builder.ConfigureAppConfiguration((context, configuration) =>
            {
                foreach (var settings in GetSettings("*"))
                {
                    configuration.AddJsonFile(settings);
                }

                foreach (var settings in GetSettings($"*.{context.HostingEnvironment.EnvironmentName}"))
                {
                    configuration.AddJsonFile(settings);
                }

                IEnumerable<string> GetSettings(string pattern)
                    => Directory.EnumerateFiles(context.HostingEnvironment.ContentRootPath, 
                        $"module.{pattern}.json", SearchOption.AllDirectories);
            });
    }
}
