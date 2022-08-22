using Confab.Shared.Abtractions.Modules;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Confab.Bootstrapper
{
    internal static class ModuleLoader
    {
        public static IList<Assembly> LoadAssemblies(IConfiguration configuration)
        {

            var assemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();
            var localtions = assemblies.Where(a => !a.IsDynamic).Select(a => a.Location).ToArray();
            var files = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll")
                .Where(a => !localtions.Contains(a, StringComparer.InvariantCultureIgnoreCase))
                .ToList();

            DisableModules(configuration, files);

            files.ForEach(f => assemblies.Add(AppDomain.CurrentDomain.Load(AssemblyName.GetAssemblyName(f))));
            return assemblies;
        }

        public static IList<IModule> LoadModules(IEnumerable<Assembly> assemblies)
        => assemblies
            .SelectMany(a => a.GetTypes())
            .Where(a => typeof(IModule).IsAssignableFrom(a) && !a.IsInterface)
            .OrderBy(a => a.Name)
            .Select(Activator.CreateInstance)
            .Cast<IModule>()
            .ToList();

        private static void DisableModules(IConfiguration configuration, List<string> files)
        {
            const string modulePart = "Confab.Modules.";

            var disabledModules = new List<string>();
            foreach (var file in files)
            {
                if (!file.Contains(modulePart))
                    continue;

                var moduleName = file.Split(modulePart)[1].Split(".")[0].ToLowerInvariant();
                var enabled = configuration.GetValue<bool>($"{moduleName}:module:enabled");

                if (!enabled)
                    disabledModules.Add(file);
            }

            foreach (var disabledModule in disabledModules)
            {
                files.Remove(disabledModule);
            }
        }
    }
}
