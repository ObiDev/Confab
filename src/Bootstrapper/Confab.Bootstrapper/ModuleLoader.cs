using Confab.Shared.Abtractions.Modules;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Confab.Bootstrapper
{
    internal static class ModuleLoader
    {
        public static IList<Assembly> LoadAssemblies()
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();
            var localtions = assemblies.Where(a => !a.IsDynamic).Select(a => a.Location).ToArray();
            var files = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll")
                .Where(a => !localtions.Contains(a, StringComparer.InvariantCultureIgnoreCase))
                .ToList();

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
    }
}
