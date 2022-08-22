using Microsoft.Extensions.Configuration;
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
